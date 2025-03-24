  // Student data (Example)
  const students = [
    { 
        name: "Ahmed Mohamed", email: "ahmed@student.com", phone: "01012345678", 
        grade: "Grade 1", parent: "Mohamed Ali", parentEmail: "mohamed@parent.com", 
        address: "Cairo", birthDate: "2005-06-12", gender: "Male", 
        photo: "https://randomuser.me/api/portraits/men/1.jpg"
    },
    { 
        name: "Sara Khaled", email: "sara@student.com", phone: "01098765432", 
        grade: "Grade 2", parent: "Khaled Hassan", parentEmail: "khaled@parent.com", 
        address: "Giza", birthDate: "2006-09-20", gender: "Female", 
        photo: "https://randomuser.me/api/portraits/women/2.jpg"
    },
    { 
        name: "John Doe", email: "john@student.com", phone: "01122334455", 
        grade: "Grade 1", parent: "Doe Senior", parentEmail: "doe@parent.com", 
        address: "Alexandria", birthDate: "2007-05-15", gender: "Male", 
        photo: "https://randomuser.me/api/portraits/men/3.jpg"
    },
    { 
        name: "Mariam Adel", email: "mariam@student.com", phone: "01555667788", 
        grade: "Grade 3", parent: "Adel Samir", parentEmail: "adel@parent.com", 
        address: "Mansoura", birthDate: "2008-08-10", gender: "Female", 
        photo: "https://randomuser.me/api/portraits/women/4.jpg"
    }
];

// Function to display students based on grade filter
function displayStudents(filterGrade = "all") {
    const tableBody = document.getElementById("studentTableBody");
    tableBody.innerHTML = ""; // Clear previous content

    students.forEach(student => {
        if (filterGrade === "all" || student.grade === filterGrade) {
            const row = `<tr>
                <td><img src="${student.photo}" class="student-img" alt="Student Photo"></td>
                <td>${student.name}</td>
                <td>${student.email}</td>
                <td>${student.phone}</td>
                <td>${student.grade}</td>
                <td>${student.parent}</td>
                <td>${student.parentEmail}</td>
                <td>${student.address}</td>
                <td>${student.birthDate}</td>
                <td>${student.gender}</td>
            </tr>`;
            tableBody.innerHTML += row;
        }
    });
}

// Load all students initially
displayStudents();

// Add event listener to filter dropdown
document.getElementById("gradeFilter").addEventListener("change", function () {
    displayStudents(this.value);
});