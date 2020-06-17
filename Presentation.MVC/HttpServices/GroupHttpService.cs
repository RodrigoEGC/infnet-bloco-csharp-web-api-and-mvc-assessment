using Domain.Model.Entities;
using Domain.Model.Interfaces.Services;
using Domain.Model.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Mvc.HttpServices
{
    public class GroupHttpService : IGroupHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly IOptionsMonitor<LibraryMusicalHttpOptions> _libraryHttpOptions;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SignInManager<IdentityUser> _signInManager;
        public GroupHttpService(
            IHttpClientFactory httpClientFactory,
            IOptionsMonitor<LibraryMusicalHttpOptions> libraryHttpOptions,
            IHttpContextAccessor httpContextAccessor,
            SignInManager<IdentityUser> signInManager)
        {
            _libraryHttpOptions = libraryHttpOptions ?? throw new ArgumentNullException(nameof(libraryHttpOptions));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _signInManager = signInManager;
            ;

            _httpClient = httpClientFactory.CreateClient(libraryHttpOptions.CurrentValue.Name) ?? throw new ArgumentNullException(nameof(httpClientFactory)); ;
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

        public async Task<IEnumerable<GroupEntity>> GetAllAsync()
        {
            var jwtSuccess = await AddAuthJwtToRequest();
            if (!jwtSuccess)
            {
                return null;
            }
            var httpResponseMessage = await _httpClient.GetAsync(_libraryHttpOptions.CurrentValue.GroupPath);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                await _signInManager.SignOutAsync();
                return null;
            }

            return JsonConvert.DeserializeObject<List<GroupEntity>>(await httpResponseMessage.Content.ReadAsStringAsync());
        }

        public async Task<GroupEntity> GetByIdAsync(int id)
        {
            var jwtSuccess = await AddAuthJwtToRequest();
            if (!jwtSuccess)
            {
                return null;
            }
            var pathWithId = $"{_libraryHttpOptions.CurrentValue.GroupPath}/{id}";
            var httpResponseMessage = await _httpClient.GetAsync(pathWithId);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<GroupEntity>(await httpResponseMessage.Content.ReadAsStringAsync());
        }
        public async Task<HttpResponseMessage> GetByIdHttpAsync(int id)
        {
            var jwtSuccess = await AddAuthJwtToRequest();
            if (!jwtSuccess)
            {
                return null;
            }
            var pathWithId = $"{_libraryHttpOptions.CurrentValue.GroupPath}/{id}";
            var httpResponseMessage = await _httpClient.GetAsync(pathWithId);

            return httpResponseMessage;
        }

        public async Task InsertAsync(GroupEntity insertedEntity)
        {
            var jwtSuccess = await AddAuthJwtToRequest();
            if (!jwtSuccess)
            {
                return;
            }
            var uriPath = $"{_libraryHttpOptions.CurrentValue.GroupPath}";

            var httpContent = new StringContent(JsonConvert.SerializeObject(insertedEntity), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _httpClient.PostAsync(uriPath, httpContent);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                await _signInManager.SignOutAsync();
            }
        }

        public async Task UpdateAsync(GroupEntity updatedEntity)
        {
            var jwtSuccess = await AddAuthJwtToRequest();
            if (!jwtSuccess)
            {
                return;
            }
            var pathWithId = $"{_libraryHttpOptions.CurrentValue.GroupPath}/{updatedEntity.Id}";

            var httpContent = new StringContent(JsonConvert.SerializeObject(updatedEntity), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _httpClient.PutAsync(pathWithId, httpContent);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                await _signInManager.SignOutAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var jwtSuccess = await AddAuthJwtToRequest();
            if (!jwtSuccess)
            {
                return;
            }
            await AddAuthJwtToRequest();
            var pathWithId = $"{_libraryHttpOptions.CurrentValue.GroupPath}/{id}";
            var httpResponseMessage = await _httpClient.DeleteAsync(pathWithId);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                await _signInManager.SignOutAsync();
            }
        }


        public async Task<bool> CheckNameAsync(string name, int id)
        {
            var jwtSuccess = await AddAuthJwtToRequest();
            if (!jwtSuccess)
            {
                return false;
            }

            await AddAuthJwtToRequest();
            var pathWithId = $"{_libraryHttpOptions.CurrentValue.GroupPath}/CheckName/{name}/{id}";
            var httpResponseMessage = await _httpClient.GetAsync(pathWithId);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                await _signInManager.SignOutAsync();
                return false;
            }

            return bool.Parse(await httpResponseMessage.Content.ReadAsStringAsync());
        }

    }
}
