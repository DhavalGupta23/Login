
$(function () {
    $('#myTable').DataTable({
        processing: true,
        serverSide: true,
        searchable: true,
        ajax: {
            url: '/Employee/GetOrganizations',
            type: 'POST',
            dataSrc: 'data',
        },
        columns: [{ data: "id" },
            { data: "name" }, { data: "email" }, { data: "password" }, { data: "mobile" },
            { data: "dob" },{ data: "gender" }, { data: "department" }, { data: "city" },
            {
                "mData": "Actions", "mRender": function (data, type, row)
                {
                    return "<a href='/Employee/Edit?id=" + row.id + "'>Edit</a> | <a href='/Employee/Delete?id=" + row.id + "'>Delete</a> | <a href='/Employee/Details?id=" + row.id +"'>Details</a>";
                }
            }
                 ]
    });
});



$("#mobile").bind("keypress", function (e) {
    var keyCode = e.which ? e.which : e.keyCode

    if (!(keyCode >= 48 && keyCode <= 57)) {

        return false;
    } else {
        return true;
    }
});
