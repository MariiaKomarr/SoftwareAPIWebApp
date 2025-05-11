const softwareUri = '/api/softwares';
let softwares = [];

function getSoftwares() {
    fetch(softwareUri)
        .then(response => response.json())
        .then(data => displaySoftwares(data))
        .catch(error => console.error('Unable to get softwares.', error));
}

function addSoftware() {
    const nameInput = document.getElementById('add-name');
    const versionInput = document.getElementById('add-version');
    const typeIdInput = document.getElementById('add-typeId');
    const authorInput = document.getElementById('add-author');
    const usageTermsInput = document.getElementById('add-usageTerms');
    const dateInput = document.getElementById('add-dateAdded');
    const annotationInput = document.getElementById('add-annotation');
    const errorMessagesDiv = document.getElementById('error-messages');

    const software = {
        name: nameInput.value.trim(),
        version: versionInput.value.trim(),
        typeId: parseInt(typeIdInput.value),
        author: authorInput.value.trim(),
        usageTerms: usageTermsInput.value.trim(),
        dateAdded: dateInput.value,
        annotation: annotationInput.value.trim()
    };

    fetch(softwareUri, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(software)
    })
        .then(async response => {
            if (response.ok) {
                getSoftwares();
                nameInput.value = '';
                versionInput.value = '';
                typeIdInput.value = '';
                authorInput.value = '';
                usageTermsInput.value = '';
                dateInput.value = '';
                annotationInput.value = '';
                errorMessagesDiv.innerHTML = '';
                alert('Software added successfully.');
            } else {
                const errorData = await response.json();
                const errors = errorData.errors || errorData;
                const errorList = Object.values(errors).flat();

                errorMessagesDiv.innerHTML = '<ul>' +
                    errorList.map(err => `<li>${err}</li>`).join('') +
                    '</ul>';

                alert('Failed to add software.');
            }
        })
        .catch(error => {
            console.error('Error while adding software:', error);
            alert('An error occurred while adding the software.');
        });
}

function deleteSoftware(id) {
    fetch(`${softwareUri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getSoftwares())
        .catch(error => console.error('Unable to delete software.', error));
}

function displayEditForm(id) {
    const software = softwares.find(s => s.softwareId === id);
    document.getElementById('edit-id').value = software.softwareId;
    document.getElementById('edit-name').value = software.name;
    document.getElementById('edit-version').value = software.version;
    document.getElementById('edit-typeId').value = software.typeId;
    document.getElementById('edit-author').value = software.author;
    document.getElementById('edit-usageTerms').value = software.usageTerms;
    document.getElementById('edit-dateAdded').value = software.dateAdded;
    document.getElementById('edit-annotation').value = software.annotation;
    document.getElementById('editForm').style.display = 'block';
}

function updateSoftware() {
    const id = document.getElementById('edit-id').value;
    const software = {
        softwareId: parseInt(id),
        name: document.getElementById('edit-name').value.trim(),
        version: document.getElementById('edit-version').value.trim(),
        typeId: parseInt(document.getElementById('edit-typeId').value),
        author: document.getElementById('edit-author').value.trim(),
        usageTerms: document.getElementById('edit-usageTerms').value.trim(),
        dateAdded: document.getElementById('edit-dateAdded').value,
        annotation: document.getElementById('edit-annotation').value.trim()
    };

    fetch(`${softwareUri}/${id}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(software)
    })
        .then(() => getSoftwares())
        .catch(error => console.error('Unable to update software.', error));

    closeInput();
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}

function displaySoftwares(data) {
    const tBody = document.getElementById('softwares');
    tBody.innerHTML = '';
    softwares = data;

    data.forEach(s => {
        let row = tBody.insertRow();
        row.insertCell(0).innerText = s.softwareId;
        row.insertCell(1).innerText = s.name;
        row.insertCell(2).innerText = s.version;
        row.insertCell(3).innerText = s.typeId;
        row.insertCell(4).innerText = s.author;
        row.insertCell(5).innerText = new Date(s.dateAdded).toLocaleString();

        let editBtn = document.createElement('button');
        editBtn.textContent = 'Edit';
        editBtn.onclick = () => displayEditForm(s.softwareId);

        let deleteBtn = document.createElement('button');
        deleteBtn.textContent = 'Delete';
        deleteBtn.onclick = () => deleteSoftware(s.softwareId);

        row.insertCell(6).appendChild(editBtn);
        row.insertCell(7).appendChild(deleteBtn);
    });
}

function installSoftware(id) {
    const studentId = document.getElementById('student-select').value;
    const installData = {
        softwareId: id,
        studentId: parseInt(studentId),
        installDate: new Date().toISOString()
    };

    fetch('/api/installations', {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(installData)
    })
        .then(response => {
            if (response.ok) {
                alert('Software installed');
            } else {
                alert('Installation failed');
            }
        })
        .catch(error => console.error('Installation error:', error));
}
