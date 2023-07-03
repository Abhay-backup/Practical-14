$(document).ready(() => {

	var isDataFound = true;

	$("#search").keyup((e) => {

		// setting the default page
		//$("#PageNo").text("1")

		//request for date
		$.ajax({
			url: "/Employee/GetDate",
			type: "GET",
			data: {
				SearchString: e.target.value,
				pageNo: 1,
			},
			success: (data) => {

				// checking if the data is null or empty
				if (data.length == 0) {
					$("#fillHere").html('<tr ><td colspan="5"> Employee Not Available</td></tr>')
				}
				else {
					var html = ""
					$("#fillHere").text("");

					// Making the html 
				
					$.each(data, function (index, item) {

						var jsonDate = item.DOB;

						var microsoftJsonDate = jsonDate;
						var timestamp = parseInt(microsoftJsonDate.match(/\d+/)[0]);
						var date = new Date(timestamp);
						var year = date.getFullYear();
						var month = String(date.getMonth() + 1).padStart(2, '0');
						var day = String(date.getDate()).padStart(2, '0');
						var formattedDate = year + '-' + month + '-' + day;


						html += "<tr><td>" + item.Id + "</td><td>" + item.Name + "</td><td>" + formattedDate + "</td><td>" + item.Age + "</td><td>" +
							"<a class='btn btn-outline-primary' href='/Employee/Edit/" + item.Id + "'>Edit</a> | " +
							"<a class='btn btn-outline-success' href='/Employee/Details/" + item.Id + "'>Details</a> | " +
							"<a class='btn btn-outline-danger' href='/Employee/Delete/" + item.Id + "'>Delete</a>"
							+ "</td></tr > "
					})
					// filling the html
					$("#fillHere").html(html);
				}
			},
			error: (e) => {
				console.log(e);
			}
		})
	})
})