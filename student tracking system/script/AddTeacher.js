/* AddTeacher.js */
document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("addTeacherForm");
    const inputs = form.querySelectorAll("input");

    form.addEventListener("submit", function (event) {
        let isValid = true;

        inputs.forEach((input) => {
            const feedback = input.nextElementSibling;
            if (!feedback || !feedback.classList.contains("invalid-feedback")) return;

            if (!input.value.trim()) {
                showError(input, "This field is required.");
                isValid = false;
            } else {
                removeError(input);
            }
        });

        if (!isValid) {
            event.preventDefault();
            return;
        }

        event.preventDefault();
        Swal.fire({
            title: "Success!",
            text: "The teacher has been registered successfully.",
            icon: "success",
            confirmButtonText: "OK",
            confirmButtonColor: "#504b38",
        }).then((result) => {
            if (result.isConfirmed) {
                form.submit();
            }
        });
    });

    function showError(input, message) {
        input.classList.add("is-invalid");
        input.nextElementSibling.textContent = message;
        input.nextElementSibling.style.display = "block";
    }

    function removeError(input) {
        input.classList.remove("is-invalid");
        input.nextElementSibling.style.display = "none";
    }
});
