﻿const installationUri = '/api/installations';
let installations = [];

function getInstallations() {
    fetch(installationUri)
        .then(response => response.json())
        .then(data => displayInstallations(data))
        .catch(error => console.error('Unable to get installations.', error));
}

function addInstallation() {
    const studentInput = document.getElementById('add-studentId');
    const softwareInput = document.getElementById('add-softwareId');
    const dateInput = document.getElementById('add-date');

    const installation = {
        studentId: parseInt(studentInput.value),
        softwareId: parseInt(softwareInput.value),
        installDate: dateInput.value
    };

    fetch(installationUri, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(installation)
    })
        .then(response => {
            if (response.ok) {
                getInstallations();
                studentInput.value = '';
                softwareInput.value = '';
                dateInput.value = '';
                alert('Installation added successfully.');
            } else {
                alert('Failed to add installation.');
            }
        })
        .catch(error => {
            console.error('Error while adding installation:', error);
            alert('An error occurred while adding the installation.');
        });
}

function deleteInstallation(id) {
    fetch(`${installationUri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getInstallations())
        .catch(error => console.error('Unable to delete installation.', error));
}

function displayEditForm(id) {
    const inst = installations.find(i => i.installId === id);
    document.getElementById('edit-id').value = inst.installId;
    document.getElementById('edit-studentId').value = inst.studentId;
    document.getElementById('edit-softwareId').value = inst.softwareId;
    document.getElementById('edit-date').value = inst.installDate;
    document.getElementById('editForm').style.display = 'block';
}

function updateInstallation() {
    const id = document.getElementById('edit-id').value;
    const installation = {
        installId: parseInt(id),
        studentId: parseInt(document.getElementById('edit-studentId').value),
        softwareId: parseInt(document.getElementById('edit-softwareId').value),
        installDate: document.getElementById('edit-date').value
    };

    fetch(`${installationUri}/${id}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(installation)
    })
        .then(() => getInstallations())
        .catch(error => console.error('Unable to update installation.', error));

    closeInput();
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}

function displayInstallations(data) {
    const tBody = document.getElementById('installations');
    tBody.innerHTML = '';
    installations = data;

    data.forEach(i => {
        let row = tBody.insertRow();
        row.insertCell(0).innerText = i.student?.name || i.studentId;
        row.insertCell(1).innerText = i.software?.name || i.softwareId;

        const parsedDate = Date.parse(i.installDate);
        const displayDate = isNaN(parsedDate) ? 'Invalid Date' : new Date(parsedDate).toLocaleString();
        row.insertCell(2).innerText = displayDate;

        let editBtn = document.createElement('button');
        editBtn.textContent = 'Edit';
        editBtn.onclick = () => displayEditForm(i.installId);

        let deleteBtn = document.createElement('button');
        deleteBtn.textContent = 'Delete';
        deleteBtn.onclick = () => deleteInstallation(i.installId);

        row.insertCell(3).appendChild(editBtn);
        row.insertCell(4).appendChild(deleteBtn);
    });
}
