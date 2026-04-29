function completeCourse(courseName){

let courses =
JSON.parse(localStorage.getItem("completedCourses")) || [];

if(!courses.includes(courseName)){

courses.push(courseName);

localStorage.setItem(
"completedCourses",
JSON.stringify(courses)
);

alert("Course Completed");

location.reload();

}else{

alert("Course already completed");

}

}

window.onload=function(){

let courses =
JSON.parse(localStorage.getItem("completedCourses")) || [];

document.querySelectorAll(".complete-btn").forEach(btn=>{

let name = btn.parentElement.querySelector("h3").textContent;

if(courses.includes(name)){

btn.disabled=true;
btn.textContent="Completed";

}

});

}