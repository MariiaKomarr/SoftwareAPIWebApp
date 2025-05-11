const studentUri = '/api/students';
let students = [];

function getStudents() {
    fetch(studentUri)
        .then(response => response.json())
        .then(data => displayStudents(data))
        .catch(error => console.error('Unable to get students.', error));
}

function addStudent() {
    const nameInput = document.getElementById('add-name');
    const emailInput = document.getElementById('add-email');
    const facultyInput = document.getElementById('add-facultyId');

    const student = {
        name: nameInput.value.trim(),
        email: emailInput.value.trim(),
        facultyId: parseInt(facultyInput.value)
    };

    fetch(studentUri, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(student)
    })
        .then(response => {
            if (response.ok) {
                getStudents();

                // Очистити поля
                nameInput.value = '';
                emailInput.value = '';
                facultyInput.value = '';

                alert('Student added successfully.');
            } else {
                alert('Failed to add student.');
            }
        })
        .catch(error => {
            console.error('Error while adding student:', error);
            alert('An error occurred while adding the student.');
        });
}


function deleteStudent(id) {
    fetch(`${studentUri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getStudents())
        .catch(error => console.error('Unable to delete student.', error));
}

function displayEditForm(id) {
    const student = students.find(s => s.studentId === id);
    document.getElementById('edit-id').value = student.studentId;
    document.getElementById('edit-name').value = student.name;
    document.getElementById('edit-email').value = student.email;
    document.getElementById('edit-facultyId').value = student.facultyId;
    document.getElementById('editForm').style.display = 'block';
}

function updateStudent() {
    const id = document.getElementById('edit-id').value;
    const student = {
        studentId: parseInt(id),
        name: document.getElementById('edit-name').value.trim(),
        email: document.getElementById('edit-email').value.trim(),
        facultyId: parseInt(document.getElementById('edit-facultyId').value)
    };

    fetch(`${studentUri}/${id}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(student)
    })
        .then(() => getStudents())
        .catch(error => console.error('Unable to update student.', error));

    closeInput();
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}

function displayStudents(data) {
    const tBody = document.getElementById('students');
    tBody.innerHTML = '';
    students = data;

    data.forEach(s => {
        let row = tBody.insertRow();
        row.insertCell(0).innerText = s.studentId;
        row.insertCell(1).innerText = s.name;
        row.insertCell(2).innerText = s.email;
        row.insertCell(3).innerText = s.faculty?.name || s.facultyId;

        let editBtn = document.createElement('button');
        editBtn.textContent = 'Edit';
        editBtn.onclick = () => displayEditForm(s.studentId);

        let deleteBtn = document.createElement('button');
        deleteBtn.textContent = 'Delete';
        deleteBtn.onclick = () => deleteStudent(s.studentId);

        row.insertCell(4).appendChild(editBtn);
        row.insertCell(5).appendChild(deleteBtn);
    });
}
