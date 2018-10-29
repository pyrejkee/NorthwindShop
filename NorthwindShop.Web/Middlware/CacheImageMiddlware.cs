using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using NorthwindShop.Web.Extensions;

namespace NorthwindShop.Web.Middlware
{
    public class CacheImageMiddlware
    {
        private readonly RequestDelegate _next;
        private readonly IDistributedCache _cache;
        private readonly IConfiguration _configuration;

        public CacheImageMiddlware(RequestDelegate next,
                                   IDistributedCache cache,
                                   IConfiguration configuration)
        {
            _next = next;
            _cache = cache;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Stream originalBody = context.Response.Body;
            try
            {
                using (var memStream = new MemoryStream())
                {
                    context.Response.Body = memStream;

                    await _next(context);

                    memStream.Position = 0;
                    var responseBody = memStream.ToArray();

                    var responseHeaders = context.Response.GetTypedHeaders();
                    if (responseHeaders.Headers.Contains(new KeyValuePair<string, StringValues>("Content-Type", "image/jpg")))
                    {
                        var maxStoredImagesCount = _configuration.GetValue<int>("ImageCaching:MaxStoredImagesCount");
                        var redisKeys = _cache.GetRedisKeys();
                        if (redisKeys.Count == maxStoredImagesCount)
                        {
                            _cache.CleanRedisStorage(redisKeys);
                        }

                        var expirationTime = _configuration.GetValue<int>("ImageCaching:CacheExpirationTime");
                        var cacheEntryOptions = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(expirationTime));
                        var id = context.GetRouteValue("id").ToString();

                        _cache.Set($"categoryId-{id}", responseBody, cacheEntryOptions);
                    }

                    memStream.Position = 0;
                    await memStream.CopyToAsync(originalBody);
                }
            }
            finally
            {
                context.Response.Body = originalBody;
            }
        }
    }
}
