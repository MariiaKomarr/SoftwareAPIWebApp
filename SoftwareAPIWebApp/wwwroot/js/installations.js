const installationUri = '/api/installations';
let installations = [];

function getInstallations() {
    fetch(installationUri)
        .then(response => response.json())
        .then(data => displayInstallations(data))
        .catch(error => console.error('Unable to get installations.', error));
}

function addInstallation() {
    const installation = {
        studentId: parseInt(document.getElementById('add-studentId').value),
        softwareId: parseInt(document.getElementById('add-softwareId').value),
        date: document.getElementById('add-date').value
    };

    fetch(installationUri, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(installation)
    })
        .then(() => getInstallations())
        .catch(error => console.error('Unable to add installation.', error));
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
    document.getElementById('edit-date').value = inst.date;
    document.getElementById('editForm').style.display = 'block';
}

function updateInstallation() {
    const id = document.getElementById('edit-id').value;
    const installation = {
        installId: parseInt(id),
        studentId: parseInt(document.getElementById('edit-studentId').value),
        softwareId: parseInt(document.getElementById('edit-softwareId').value),
        date: document.getElementById('edit-date').value
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
        row.insertCell(2).innerText = new Date(i.date).toLocaleString();

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
