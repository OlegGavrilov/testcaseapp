using TestCaseApp.Dto;

namespace TestCaseApp.Mappers;

public static class TestCaseUserToDto
{
    public static TestCaseAppUserDto ToDto(this TestCaseAppUser user) =>
        new TestCaseAppUserDto(user.Name, user.Surname, user.Phone, user.Email, user.UserName);
}