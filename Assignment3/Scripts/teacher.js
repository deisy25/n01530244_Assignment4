function AddTeacher() {
	//goal: send a request which looks like this:
	//POST : http://localhost:52578/api/TeacherData/AddTeacher
	//with POST data 

	var URL = "http://localhost:52578/api/TeacherData/AddTeacher/";

	var rq = new XMLHttpRequest();

	var teacherFname = document.getElementById('teacherFname').value;
	var teacherlname = document.getElementById('teacherlname').value;
	var teacherNumber = document.getElementById('teacherNumber').value;
	var salary = document.getElementById('salary').value;

	var TeacherData = {
		"teacherFname": teacherFname,
		"teacherlname": teacherlname,
		"teacherNumber": teacherNumber,
		"salary": salary
	};

	rq.open("POST", URL, true);
	rq.setRequestHeader("Content-Type", "application/json");
	rq.onreadystatechange = function () {
		//ready state should be 4 AND status should be 200
		if (rq.readyState == 4 && rq.status == 200) {
			//request is successful and the request is finished
			
		}

	}
	//POST information sent through the .send() method
	rq.send(JSON.stringify(TeacherData));

}

function UpdateTeacher(TeacherId) {
	var URL = "http://localhost:52578/api/TeacherData/UpdateTeacher/" + TeacherId;

	var rq = new XMLHttpRequest();

	var teacherFname = document.getElementById('teacherFname').value;
	var teacherlname = document.getElementById('teacherlname').value;
	var teacherNumber = document.getElementById('teacherNumber').value;
	var salary = document.getElementById('salary').value;

	var TeacherData = {
		"teacherFname": teacherFname,
		"teacherlname": teacherlname,
		"teacherNumber": teacherNumber,
		"salary": salary
	};


	rq.open("POST", URL, true);
	rq.setRequestHeader("Content-Type", "application/json");
	rq.onreadystatechange = function () {
		//ready state should be 4 AND status should be 200
		if (rq.readyState == 4 && rq.status == 200) {
			//request is successful and the request is finished

		}

	}
	//POST information sent through the .send() method
	rq.send(JSON.stringify(TeacherData));
}