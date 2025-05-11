const facultyUri = '/api/faculties';
let faculties = [];

function getFaculties() {
    fetch(facultyUri)
        .then(response => response.json())
        .then(data => displayFaculties(data))
        .catch(error => console.error('Unable to get faculties.', error));
}

function addFaculty() {
    const nameInput = document.getElementById('add-name');
    const deanInput = document.getElementById('add-dean');
    const faculty = {
        name: nameInput.value.trim(),
        dean: deanInput.value.trim()    };

    fetch(facultyUri, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(faculty)
    })
        .then(response => {
            if (response.ok) {
                getFaculties();
                nameInput.value = '';
                deanInput.value = ''; 
                alert('Faculty added successfully.');
            } else {
                alert('Failed to add faculty.');
            }
        })
        .catch(error => {
            console.error('Error while adding faculty:', error);
            alert('An error occurred while adding the faculty.');
        });
}

function deleteFaculty(id) {
    fetch(`${facultyUri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getFaculties())
        .catch(error => console.error('Unable to delete faculty.', error));
}

function displayEditForm(id) {
    id = parseInt(id);
    const faculty = faculties.find(f => f.facultyId === id); 
    document.getElementById('edit-id').value = faculty.facultyId; 
    document.getElementById('edit-name').value = faculty.name;
    document.getElementById('edit-dean').value = faculty.dean;
    document.getElementById('editForm').style.display = 'block';
}

function updateFaculty() {
    const id = document.getElementById('edit-id').value;
    const name = document.getElementById('edit-name').value.trim();
    const dean = document.getElementById('edit-dean').value.trim();

    fetch(`${facultyUri}/${id}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ facultyId: parseInt(id), name: name, dean: dean }) // 👈 змінено "id" → "facultyId"
    })
        .then(response => {
            if (response.ok) {
                getFaculties();
                closeInput();
                alert('Faculty updated successfully.');
            } else {
                alert('Failed to update faculty.');
            }
        })
        .catch(error => {
            console.error('Error while updating faculty:', error);
            alert('An error occurred while updating the faculty.');
        });
}



function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}

function displayFaculties(data) {
    const tBody = document.getElementById('faculties');
    tBody.innerHTML = '';
    faculties = data;

    data.forEach(f => {
        let row = tBody.insertRow();
        row.insertCell(0).innerText = f.facultyId;
        row.insertCell(1).innerText = f.name;
        row.insertCell(2).innerText = f.dean;

        let editBtn = document.createElement('button');
        editBtn.textContent = 'Edit';
        editBtn.onclick = () => displayEditForm(f.facultyId); 

        let deleteBtn = document.createElement('button');
        deleteBtn.textContent = 'Delete';
        deleteBtn.onclick = () => deleteFaculty(f.facultyId); 

        row.insertCell(3).appendChild(editBtn);
        row.insertCell(4).appendChild(deleteBtn);
    });
}
