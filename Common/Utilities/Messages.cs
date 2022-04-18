
namespace Common
{
    public static class Messages
    {
        public static string Completed = "Transaction Completed!";
        public static string Failed = "An Error has been occured!";
        public static string DuplicateUserEmail = "This email has been taken by another user!";
        public static string DuplicateRole = "This role has already been created!";
        public static string DuplicateCompanyUser = "This user has already been created!";
        public static string MissingUserEmail = "We couldn't find a user with this email!";
        public static string MissingUserId = "We couldn't find a user with this id!";
        public static string MissingUserRole = "We couldn't find this role!";
        public static string MissingCompanyUserCanCreate = "We couldn't find a user which can create a company!";
        public static string MissingCompanyById = "We couldn't find a company with this id!";
        public static string MissingCompanyUserById = "We couldn't find a company user with this id!";
        public static string UserHasThisRole = "Role has already been defined for the user!";
        public static string InvalidPassword = "Your password is invalid!";
        public static string WrongModel = "Your model is wrong";
        public static string NoRole = "There is no role in the records!";
        public static string Deleted = "Delete transaction has been completed!";
        public static string Unauthorized = "You dont have permission to see this page!";
        public static string CompanyCreatedButRoleDidntAssign = "Your company has been created but your role didnt assign. Inform the administrator if you can customize your company!";
    }
}
