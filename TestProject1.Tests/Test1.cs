using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;
using GymManagementSystem.Models;
using System.Collections.Generic;
using System.Linq;

namespace GymManagementSystem.Tests
{
    [TestClass]
    public class AdminTests
    {
        // Helper method to validate the Admin model
        private List<ValidationResult> ValidateAdmin(Admin admin)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(admin, null, null);
            Validator.TryValidateObject(admin, validationContext, validationResults, true);
            return validationResults;
        }

        [TestMethod]
        public void Admin_UsernameIsRequired_ShouldReturnValidationError()
        {
            // Arrange
            var admin = new Admin
            {
                Username = "", // Empty username
                Password = "ValidPassword123"
            };

            // Act
            var validationResults = ValidateAdmin(admin);

            // Assert
            Assert.IsTrue(validationResults.Any(v => v.ErrorMessage.Contains("Username is required")));
        }

        [TestMethod]
        public void Admin_PasswordIsRequired_ShouldReturnValidationError()
        {
            // Arrange
            var admin = new Admin
            {
                Username = "ValidUsername",
                Password = "" // Empty password
            };

            // Act
            var validationResults = ValidateAdmin(admin);

            // Assert
            Assert.IsTrue(validationResults.Any(v => v.ErrorMessage.Contains("Password is required")));
        }

        [TestMethod]
        public void Admin_PasswordTooShort_ShouldReturnValidationError()
        {
            // Arrange
            var admin = new Admin
            {
                Username = "ValidUsername",
                Password = "Short" // Password length < 6
            };

            // Act
            var validationResults = ValidateAdmin(admin);

            // Assert
            Assert.IsTrue(validationResults.Any(v => v.ErrorMessage.Contains("Password must be at least 6 characters long")));
        }

        [TestMethod]
        public void Admin_ValidModel_ShouldPassValidation()
        {
            // Arrange
            var admin = new Admin
            {
                Username = "ValidUsername",
                Password = "ValidPassword123" // Valid password
            };

            // Act
            var validationResults = ValidateAdmin(admin);

            // Assert
            Assert.IsFalse(validationResults.Any()); // Should not have any validation errors
        }
    }
}
