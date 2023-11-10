using Application.Features.Users.Constants;
using Application.Features.Users.Rules;
using Application.Services.ImageService;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Concrete;
using Domain.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Domain.Constants.PathConstant;

namespace Application.Services.UserService;

public interface IUserService
{
    Task<User?> GetByEmail(string email);
    Task<User> GetById(int id);
    Task<User> Update(User user);
    Task<string> UpdateImage(IFormFile file, string imageUrl);
    Task ActivateTheUser(int userId);
    Task<string> UpdateCV(IFormFile file, string cvUrl);
}
public class UserManager:IUserService
{
    private readonly IUserRepository _userDal;
    private readonly ImageServiceBase _imageService;
    private readonly UserBusinessRules _userBusinessRules;

    public UserManager(IUserRepository userDal, ImageServiceBase imageService, UserBusinessRules userBusinessRules)
    {
        _userDal = userDal;
        _imageService = imageService;
        _userBusinessRules = userBusinessRules;
    }

    public async Task ActivateTheUser(int userId)
    {
        User? user = await _userDal.GetAsync(p => p.Id == userId);
        if (user is null) 
            throw new BusinessException(UserMessages.UserNotFound);

        user.UserStatus = true;
        await _userDal.UpdateAsync(user);
    }

    public async Task<User?> GetByEmail(string email)
    {
        User? user = await _userDal.GetAsync(u => u.Email == email);

        await _userBusinessRules.UserShouldBeExist(user);

        return user;
    }

    public async Task<User> GetById(int id)
    {
        // Retrieves a user from the data access layer (DAL) based on the provided ID
        User? user = await _userDal.GetAsync(u => u.Id == id);
        return user;
    }

    public async Task<User> Update(User user)
    {
        // Updates the user in the data access layer (DAL)
        User updatedUser = await _userDal.UpdateAsync(user);
        return updatedUser;
    }

    public async Task<string> UpdateImage(IFormFile file, string? imageUrl)
    {
        if(imageUrl == DEFAULT_IMAGE_URL)
        {
            imageUrl = await _imageService.UploadAsync(file);
        }
        else
        {
            imageUrl = await _imageService.UpdateAsync(file, imageUrl);
        }
        return imageUrl;
    }

    public async Task<string> UpdateCV(IFormFile file, string? cvUrl)
    {
        if(cvUrl is null)
        {
            cvUrl = await _imageService.UploadAsync(file);
        }
        else
        {
            cvUrl = await _imageService.UpdateAsync(file, cvUrl);
        }
        return cvUrl;
    }


}
