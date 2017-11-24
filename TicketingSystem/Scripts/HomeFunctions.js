function filterTickets() {
    var input, filter, table, tr, title, body, author, contains, i;

    var statusCombo = $('#statusFilter')[0];
    var statusFilter = statusCombo.options[statusCombo.value].text;
    input = document.getElementById("searchBox");
    filter = input.value.toUpperCase();
    table = document.getElementById("iTicketsTable");
    tr = table.getElementsByTagName("tr");
    for (i = 0; i < tr.length; i++) {
        title = tr[i].getElementsByTagName("td")[0];
        if (title) {
            body = tr[i].getElementsByTagName("td")[1];
            author = tr[i].getElementsByTagName("td")[3];
            contains = containsFilter(title, filter) || containsFilter(body, filter) || containsFilter(author, filter);
            if (contains && statusFilter === tr[i].className) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}

function containsFilter(element, filter) {
    return element.innerHTML.toUpperCase().indexOf(filter) > -1
}

$(function () {
    var statusFilter = $('#statusFilter')[0];
    if (statusFilter !== undefined) {
        statusFilter.selectedIndex = 0;
    }
});