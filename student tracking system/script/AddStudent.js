document.addEventListener("DOMContentLoaded", function () {
    const form = document.querySelector("form");
    const inputs = form.querySelectorAll("input, select");
    const phoneRegex = /^(010|011|012|015)\d{8}$/;
    const passwordRegex = /^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$/;

    form.addEventListener("submit", function (event) {
        let isValid = true;

        inputs.forEach((input) => {
            const feedback = input.nextElementSibling; // `div.invalid-feedback`
            if (!feedback || !feedback.classList.contains("invalid-feedback")) return;

            if (input.type === "text" || input.type === "email" || input.type === "date") {
                if (!input.value.trim()) {
                    showError(input, "This field is required.");
                    isValid = false;
                } else {
                    removeError(input);
                }
            }

            if (input.type === "tel") {
                if (!phoneRegex.test(input.value.trim())) {
                    showError(input, "Phone number must be 11 digits and start with 010, 011, 012, or 015.");
                    isValid = false;
                } else {
                    removeError(input);
                }
            }

            if (input.id === "password") {
                if (!passwordRegex.test(input.value.trim())) {
                    showError(input, "Password must be at least 6 characters long, containing letters, numbers, and a special character.");
                    isValid = false;
                } else {
                    removeError(input);
                }
            }

            if (input.tagName === "SELECT") {
                if (!input.value) {
                    showError(input, "Please select a grade.");
                    isValid = false;
                } else {
                    removeError(input);
                }
            }
        });

        // Validate gender selection
        const genderInputs = document.querySelectorAll("input[name='gender']");
        const genderFeedback = genderInputs[genderInputs.length - 1].parentElement.nextElementSibling;

        if (![...genderInputs].some((input) => input.checked)) {
            showError(genderInputs[0], "Please select a gender.", genderFeedback);
            isValid = false;
        } else {
            removeError(genderInputs[0], genderFeedback);
        }

        if (!isValid) {
            event.preventDefault(); // Prevent form submission if validation fails
            return;
        }

        event.preventDefault();
        Swal.fire({
            title: "Success!",
            text: "The student has been registered successfully.",
            icon: "success",
            confirmButtonText: "OK",
            confirmButtonColor: "#504b38",
            showCancelButton: true,
            customClass: {
                popup: "custom-popup",
                icon: "custom-success-icon",
                confirmButton: "custom-confirm-btn",
                cancelButton: "custom-cancel-btn"
            }
        }).then((result) => {
            if (result.isConfirmed) {
                form.submit(); 
            }
        });
    });

    function showError(input, message, feedbackElement = null) {
        const feedback = feedbackElement || input.nextElementSibling;
        input.classList.add("is-invalid");
        feedback.textContent = message;
        feedback.style.display = "block";
    }

    function removeError(input, feedbackElement = null) {
        const feedback = feedbackElement || input.nextElementSibling;
        input.classList.remove("is-invalid");
        feedback.style.display = "none";
    }
});
