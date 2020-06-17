using Domain.Model.Entities;
using Domain.Model.Interfaces.Services;
using Domain.Model.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Mvc.HttpServices
{
    public class AlbumHttpService : IAlbumService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IOptionsMonitor<LibraryMusicalHttpOptions> _libraryHttpOptions;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AlbumHttpService(
            IHttpClientFactory httpClientFactory,
            IOptionsMonitor<LibraryMusicalHttpOptions> libraryHttpOptions,
            IHttpContextAccessor httpContextAccessor,
            SignInManager<IdentityUser> signInManager)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _libraryHttpOptions = libraryHttpOptions ?? throw new ArgumentNullException(nameof(libraryHttpOptions));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _signInManager = signInManager;

            _httpClient = httpClientFactory.CreateClient(libraryHttpOptions.CurrentValue.Name);
            _httpClient.Timeout = TimeSpan.FromDays(_libraryHttpOptions.CurrentValue.DayOut);
        }

        private async Task<bool> AddAuthJwtToRequest()
        {
            var jwtCookieExists = _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue("libraryToken", out var jwtFromCookie);
            if (!jwtCookieExists)
            {
                await _signInManager.SignOutAsync();
                return false;
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtFromCookie);
            return true;
        }
        public async Task DeleteAsync(int id)
        {
            var jwtSuccess = await AddAuthJwtToRequest();
            if (!jwtSuccess)
            {
                return;
            }
            await AddAuthJwtToRequest();
            var pathWithId = $"{_libraryHttpOptions.CurrentValue.AlbumPath}/{id}";
            var httpResponseMessage = await _httpClient.DeleteAsync(pathWithId);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                await _signInManager.SignOutAsync();
            }
        }

        public async Task<IEnumerable<AlbumEntity>> GetAllAsync()
        {
            var jwtSuccess = await AddAuthJwtToRequest();
            if (!jwtSuccess)
            {
                return null;
            }
            var httpResponseMessage = await _httpClient.GetAsync(_libraryHttpOptions.CurrentValue.AlbumPath);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<AlbumEntity>>(await httpResponseMessage.Content
                    .ReadAsStringAsync());
            }

            if (httpResponseMessage.StatusCode == HttpStatusCode.Forbidden)
            {
                await _signInManager.SignOutAsync();
            }

            return null;
        }

        public async Task<AlbumEntity> GetByIdAsync(int id)
        {
            var jwtSuccess = await AddAuthJwtToRequest();
            if (!jwtSuccess)
            {
                return null;
            }
            var pathWithId = $"{_libraryHttpOptions.CurrentValue.AlbumPath}/{id}";
            var httpResponseMessage = await _httpClient.GetAsync(pathWithId);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<AlbumEntity>(await httpResponseMessage.Content
                    .ReadAsStringAsync());
            }

            if (httpResponseMessage.StatusCode == HttpStatusCode.Forbidden)
            {
                await _signInManager.SignOutAsync();
                new RedirectToActionResult("Album", "Index", null);
            }

            return null;
        }

        public async Task InsertAsync(AlbumEntity insertedEntity)
        {
            var jwtSuccess = await AddAuthJwtToRequest();
            if (!jwtSuccess)
            {
                return;
            }
            var uriPath = $"{_libraryHttpOptions.CurrentValue.AlbumPath}";

            var httpContent = new StringContent(JsonConvert.SerializeObject(insertedEntity), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _httpClient.PostAsync(uriPath, httpContent);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                await _signInManager.SignOutAsync();
            }
        }

        public async Task UpdateAsync(AlbumEntity updatedEntity)
        {
            var jwtSuccess = await AddAuthJwtToRequest();
            if (!jwtSuccess)
            {
                return;
            }
            var pathWithId = $"{_libraryHttpOptions.CurrentValue.AlbumPath}/{updatedEntity.Id}";

            var httpContent = new StringContent(JsonConvert.SerializeObject(updatedEntity), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _httpClient.PutAsync(pathWithId, httpContent);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                await _signInManager.SignOutAsync();
            }
        }
    }
}
