document.querySelectorAll(".role-option-label input").forEach((input) => {
  input.addEventListener("change", function () {
    document
      .querySelectorAll(".role-option-label span")
      .forEach((span) => span.classList.remove("active"));

    this.nextElementSibling.classList.add("active");
  });
});

  document
    .getElementById("togglePassword")
    .addEventListener("click", function () {
      let passwordInput = document.getElementById("password");
      let icon = this;

      if (passwordInput.type === "password") {
        passwordInput.type = "text";
        icon.classList.remove("bi-eye-slash");
        icon.classList.add("bi-eye");
      } else {
        passwordInput.type = "password";
        icon.classList.remove("bi-eye");
        icon.classList.add("bi-eye-slash");
      }
    });