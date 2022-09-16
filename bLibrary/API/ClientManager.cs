using bLibraryAPI.Models;
using bLibraryAPI.Models.Identity;
using Newtonsoft.Json;

namespace bLibrary.API
{
    internal sealed class ClientManager
    {
        private string Host { get; }
        private int Timeout { get; }
        public ClientManager(int timeout = 86400000)
        {
            Host = "https://localhost:7282";
            Timeout = timeout;
        }
        #region Books
        public IEnumerable<Book> GetBooks()
        {
            var request = CreateRequest();
            var response = request.GetAsync($"{Host}/api/book/").Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<IEnumerable<Book>>().Result;
            else
                return null;
        }
        public Book GetBookById(Guid? Id)
        {
            if (!Id.HasValue)
                return null;

            var request = CreateRequest();
            var response = request.GetAsync($"{Host}/api/book/{Id.Value}").Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<Book>().Result;
            else
                return null;
        }
        public Book CreateBook(Book book)
        {
            var request = CreateRequest();
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(book));
            var response = request.PostAsync($"{Host}/api/book/", httpContent).Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<Book>().Result;
            else
                return null;
        }
        public Book EditBook(Book book)
        {
            var request = CreateRequest();
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(book));
            var response = request.PutAsync($"{Host}/api/book/", httpContent).Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<Book>().Result;
            else
                return null;
        }
        public Book DeleteBook(Guid? Id)
        {
            if (!Id.HasValue)
                return null;

            var request = CreateRequest();
            var response = request.DeleteAsync($"{Host}/api/book/{Id.Value}").Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<Book>().Result;
            else
                return null;
        }
        #endregion
        #region Views
        public IEnumerable<Review> GetReviews()
        {
            var request = CreateRequest();
            var response = request.GetAsync($"{Host}/api/review/").Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<IEnumerable<Review>>().Result;
            else
                return null;
        }
        public Review GetReviewById(Guid? Id)
        {
            if (!Id.HasValue)
                return null;

            var request = CreateRequest();
            var response = request.GetAsync($"{Host}/api/review/{Id.Value}").Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<Review>().Result;
            else
                return null;
        }
        public Review CreateReview(Review review)
        {
            var request = CreateRequest();
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(review));
            var response = request.PostAsync($"{Host}/api/review/", httpContent).Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<Review>().Result;
            else
                return null;
        }
        public Review EditReview(Review review)
        {
            var request = CreateRequest();
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(review));
            var response = request.PutAsync($"{Host}/api/review/", httpContent).Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<Review>().Result;
            else
                return null;
        }
        public Review DeleteReview(Guid? Id)
        {
            if (!Id.HasValue)
                return null;

            var request = CreateRequest();
            var response = request.DeleteAsync($"{Host}/api/review/{Id.Value}").Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<Review>().Result;
            else
                return null;
        }
        #endregion
        #region Genres
        public IEnumerable<Genre> GetGenres()
        {
            var request = CreateRequest();
            var response = request.GetAsync($"{Host}/api/genre/").Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<IEnumerable<Genre>>().Result;
            else
                return null;
        }
        public Genre GetGenreById(Guid? Id)
        {
            if (!Id.HasValue)
                return null;

            var request = CreateRequest();
            var response = request.GetAsync($"{Host}/api/genre/{Id.Value}").Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<Genre>().Result;
            else
                return null;
        }
        public Genre CreateGenre(Genre genre)
        {
            var request = CreateRequest();
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(genre));
            var response = request.PostAsync($"{Host}/api/genre/", httpContent).Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<Genre>().Result;
            else
                return null;
        }
        public Genre EditGenre(Genre genre)
        {
            var request = CreateRequest();
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(genre));
            var response = request.PutAsync($"{Host}/api/genre/", httpContent).Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<Genre>().Result;
            else
                return null;
        }
        public Genre DeleteGenre(Guid? Id)
        {
            if (!Id.HasValue)
                return null;

            var request = CreateRequest();
            var response = request.DeleteAsync($"{Host}/api/genre/{Id.Value}").Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<Genre>().Result;
            else
                return null;
        }
        #endregion
        #region Users
        public IEnumerable<User> GetUsers()
        {
            var request = CreateRequest();
            var response = request.GetAsync($"{Host}/api/user/").Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<IEnumerable<User>>().Result;
            else
                return null;
        }
        public User GetUserById(string Id)
        {
            if (!Guid.TryParse(Id, out _))
                return null;

            var request = CreateRequest();
            var response = request.GetAsync($"{Host}/api/user/{Id}").Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<User>().Result;
            else
                return null;
        }
        public User EditUser(User user)
        {
            var request = CreateRequest();
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(user));
            var response = request.PutAsync($"{Host}/api/user/", httpContent).Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<User>().Result;
            else
                return null;
        }
        #endregion
        private HttpClient CreateRequest()
        {
            return new HttpClient()
            {
                Timeout = new TimeSpan(Timeout)
            };
        }
    }
}
