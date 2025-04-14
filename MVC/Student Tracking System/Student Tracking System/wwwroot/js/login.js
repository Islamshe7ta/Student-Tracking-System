// wwwroot/js/login.js
document.addEventListener('DOMContentLoaded', function () {
    // Initialize particles.js with error handling
    try {
        if (typeof particlesJS === 'function' && document.getElementById('particles-js')) {
            particlesJS("particles-js", {
                particles: {
                    number: { value: 80, density: { enable: true, value_area: 800 } },
                    color: { value: "#5c7aea" },
                    shape: { type: "circle", stroke: { width: 0, color: "#000000" } },
                    opacity: {
                        value: 0.3,
                        random: true,
                        anim: { enable: true, speed: 1, opacity_min: 0.1, sync: false }
                    },
                    size: {
                        value: 3,
                        random: true,
                        anim: { enable: true, speed: 2, size_min: 0.1, sync: false }
                    },
                    line_linked: {
                        enable: true,
                        distance: 150,
                        color: "#5c7aea",
                        opacity: 0.2,
                        width: 1
                    },
                    move: {
                        enable: true,
                        speed: 1,
                        direction: "none",
                        random: true,
                        straight: false,
                        out_mode: "out",
                        bounce: false,
                        attract: { enable: false, rotateX: 600, rotateY: 1200 }
                    }
                },
                interactivity: {
                    detect_on: "canvas",
                    events: {
                        onhover: { enable: true, mode: "grab" },
                        onclick: { enable: true, mode: "push" },
                        resize: true
                    },
                    modes: {
                        grab: { distance: 140, line_linked: { opacity: 0.5 } },
                        push: { particles_nb: 4 }
                    }
                },
                retina_detect: true
            });
        }
    } catch (error) {
        console.error("Particles.js initialization error:", error);
    }

    // Enhanced role selection with animation
    document.querySelectorAll(".role-option-label input").forEach((input) => {
        input.addEventListener("change", function () {
            document.querySelectorAll(".role-option-content").forEach((content) => {
                content.style.transform = '';
                content.style.boxShadow = '';
            });

            if (this.checked) {
                const content = this.nextElementSibling;
                content.style.transform = 'translateY(-5px)';
                content.style.boxShadow = '0 5px 15px rgba(92, 122, 234, 0.3)';
            }
        });
    });

    // Password toggle with enhanced UX
    const togglePassword = document.getElementById("togglePassword");
    if (togglePassword) {
        togglePassword.addEventListener("click", function (e) {
            e.preventDefault();
            const passwordInput = document.getElementById("password");
            const icon = this.querySelector('i');

            if (!passwordInput) return;

            // Toggle password visibility
            const isPassword = passwordInput.type === "password";
            passwordInput.type = isPassword ? "text" : "password";

            // Toggle icon
            icon.classList.toggle("bi-eye-slash", !isPassword);
            icon.classList.toggle("bi-eye", isPassword);

            // Add animation with callback
            this.style.transform = 'scale(1.1)';
            setTimeout(() => {
                this.style.transform = '';
            }, 200);
        });
    }

    // Password strength indicator with improved logic
    const passwordInput = document.getElementById("password");
    if (passwordInput) {
        passwordInput.addEventListener("input", function (e) {
            const password = e.target.value;
            const progress = document.querySelector(".progress-bar");
            const text = document.querySelector(".strength-text");

            if (!progress || !text) return;

            // Enhanced strength calculation
            let strength = 0;
            const hasLength = password.length > 0;
            const hasMediumLength = password.length > 5;
            const hasStrongLength = password.length > 8;
            const hasUpper = /[A-Z]/.test(password);
            const hasNumber = /\d/.test(password);
            const hasSpecial = /[^A-Za-z0-9]/.test(password);

            // Weighted scoring
            strength += hasLength ? 10 : 0;
            strength += hasMediumLength ? 20 : 0;
            strength += hasStrongLength ? 30 : 0;
            strength += hasUpper ? 20 : 0;
            strength += hasNumber ? 10 : 0;
            strength += hasSpecial ? 10 : 0;

            // Cap at 100% and update UI
            strength = Math.min(strength, 100);
            progress.style.width = strength + '%';

            // Update appearance based on strength
            if (strength < 40) {
                progress.className = 'progress-bar bg-danger';
                text.textContent = 'Weak';
            } else if (strength < 70) {
                progress.className = 'progress-bar bg-warning';
                text.textContent = 'Medium';
            } else {
                progress.className = 'progress-bar bg-success';
                text.textContent = 'Strong';
            }
        });
    }

    // Form submission handler with improved UX
    const loginForm = document.querySelector('.login-form');
    if (loginForm) {
        loginForm.addEventListener('submit', function (e) {
            const btn = this.querySelector('button[type="submit"]');
            if (!btn) return;

            // Show loading state
            btn.innerHTML = `
                <span class="btn-text">Authenticating</span>
                <span class="btn-icon">
                    <i class="bi bi-arrow-repeat spin"></i>
                </span>
            `;
            btn.classList.add('loading');
            btn.disabled = true;
            this.classList.add('authenticating');

            // Add spin animation
            const spinIcon = btn.querySelector('.spin');
            if (spinIcon) {
                spinIcon.style.animation = 'spin 1s linear infinite';
            }
        });
    }

    // Add CSS for spin animation if not already present
    if (!document.querySelector('style#spin-animation')) {
        const style = document.createElement('style');
        style.id = 'spin-animation';
        style.textContent = `
            @keyframes spin {
                0% { transform: rotate(0deg); }
                100% { transform: rotate(360deg); }
            }
        `;
        document.head.appendChild(style);
    }
});