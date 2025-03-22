// /* script.js */
// document.addEventListener("DOMContentLoaded", () => {
//     const form = document.getElementById("addTeacherForm");
//     const teacherList = document.getElementById("teacherList");
    
//     form?.addEventListener("submit", (e) => {
//         e.preventDefault();
//         const name = document.getElementById("name").value;
//         const email = document.getElementById("email").value;
        
//         if (name && email) {
//             localStorage.setItem(email, name);
//             alert("Teacher added successfully!");
//             form.reset();
//         }
//     });
    
//     if (teacherList) {
//         for (let i = 0; i < localStorage.length; i++) {
//             const key = localStorage.key(i);
//             const name = localStorage.getItem(key);
//             const li = document.createElement("li");
//             li.textContent = `${name} - ${key}`;
//             teacherList.appendChild(li);
//         }
//     }
// });

document.addEventListener("DOMContentLoaded", () => {
    const teacherList = document.getElementById("teacherList");

    if (teacherList) {
        teacherList.innerHTML = ""; // تنظيف القائمة قبل الإضافة
        for (let i = 0; i < localStorage.length; i++) {
            const key = localStorage.key(i);
            const name = localStorage.getItem(key);

            // التأكد من أن البيانات المخزنة تتعلق بالمعلمين فقط
            if (key.includes("@") && name.length > 2) { 
                const li = document.createElement("li");
                li.textContent = `${name} - ${key}`;
                teacherList.appendChild(li);
            }
        }
    }
});
