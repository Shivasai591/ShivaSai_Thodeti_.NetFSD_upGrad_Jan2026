window.onload = () => {

  /* ============================
  Load User Name
  ============================ */

  let name = localStorage.getItem("username");

  if(name){
    document.getElementById("username").textContent = name;
    document.getElementById("nameInput").value = name;
  }else{
    document.getElementById("username").textContent = "Not set";
  }


  /* ============================
  Load Completed Courses
  ============================ */

  let courses =
  JSON.parse(localStorage.getItem("completedCourses")) || [];

  let ul = document.getElementById("courses");

  ul.innerHTML = "";

  courses.forEach(course => {

    let li = document.createElement("li");

    li.textContent = course;

    ul.appendChild(li);

  });


  /* ============================
  Load Quiz Results
  ============================ */

  let result = JSON.parse(localStorage.getItem("quizResult"));

  if(result){

    document.getElementById("score").textContent =
    `Score: ${result.score} | Percentage: ${result.percentage}% | Grade: ${result.grade}`;

  }

};


/* ============================
Save Name Function
============================ */

function saveName(){

  let name = document.getElementById("nameInput").value.trim();

  if(name === ""){
    alert("Please enter your name");
    return;
  }

  localStorage.setItem("username", name);

  document.getElementById("username").textContent = name;

  alert("Name saved successfully!");

}