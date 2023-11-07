using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UnityApp.Models;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    private string BaseApi = "http://localhost:5058";
    public async Task<IEnumerable<Author>> GetAllAuthors()
    {
        var response = await _httpClient.GetAsync("http://localhost:5058/api/authors");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var authors = JsonSerializer.Deserialize<IEnumerable<Author>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return authors;
        }

        // Handle unsuccessful API request
        throw new Exception($"Failed to retrieve authors. Status code: {response.StatusCode}");
    }
    public async Task DeleteAuthor(int authorId)
    {
        var response = await _httpClient.DeleteAsync(BaseApi + $"/api/authors/{authorId}");

        if (!response.IsSuccessStatusCode)
        {
            // Handle unsuccessful API request
            throw new Exception($"Failed to delete author. Status code: {response.StatusCode}");
        }
    }

    public async Task AddAuthor(AuthorDTO authorDto)
    {
        var json = JsonSerializer.Serialize(authorDto);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(BaseApi + "/api/authors", content);
        response.EnsureSuccessStatusCode();
    }
    public async Task<AuthorDTO> GetAuthorForEdit(int id)
    {
        var response = await _httpClient.GetAsync($"{BaseApi}/api/authors/{id}");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var author = JsonSerializer.Deserialize<AuthorDTO>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return author;
        }

        // Handle unsuccessful API request
        throw new Exception($"Failed to retrieve author for editing. Status code: {response.StatusCode}");
    }

    public async Task UpdateAuthor(int id, AuthorDTO authorDto)
    {
        var json = JsonSerializer.Serialize(authorDto);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync($"{BaseApi}/api/authors/{id}", content);
        response.EnsureSuccessStatusCode();
    }


    public async Task<IEnumerable<Book>> GetAllBooks()
    {
        var response = await _httpClient.GetAsync("http://localhost:5058/api/books");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var books = JsonSerializer.Deserialize<IEnumerable<Book>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return books;
        }

        // Handle unsuccessful API request
        throw new Exception($"Failed to retrieve books. Status code: {response.StatusCode}");
    }

    // Other methods for creating, updating, and deleting data can be added similarly
}
