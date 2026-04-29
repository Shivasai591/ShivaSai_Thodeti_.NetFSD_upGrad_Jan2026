/* ============================
Quiz Questions (15 Questions)
============================ */

const quizQuestions = [

{
question:"What is JavaScript?",
options:["Programming Language","Database","Server"],
answer:0
},

{
question:"Which tag creates a hyperlink?",
options:["<a>","<div>","<p>"],
answer:0
},

{
question:"Which CSS layout system is 2D?",
options:["Flexbox","Grid","Float"],
answer:1
},

{
question:"Which company developed JavaScript?",
options:["Microsoft","Netscape","Google"],
answer:1
},

{
question:"Which symbol is used for single-line comments in JavaScript?",
options:["//","#","<!-- -->"],
answer:0
},

{
question:"Which HTML tag creates a paragraph?",
options:["<p>","<h1>","<span>"],
answer:0
},

{
question:"Which CSS property changes text color?",
options:["text-color","color","font-color"],
answer:1
},

{
question:"Which keyword declares a variable in JavaScript?",
options:["var","int","letvar"],
answer:0
},

{
question:"Which method prints output to the console?",
options:["console.log()","print()","display()"],
answer:0
},

{
question:"Which HTML tag inserts an image?",
options:["<img>","<image>","<picture>"],
answer:0
},

{
question:"Which CSS property adds space inside an element?",
options:["margin","padding","border"],
answer:1
},

{
question:"Which property makes text bold?",
options:["font-style","font-weight","text-bold"],
answer:1
},

{
question:"Which HTML element creates an unordered list?",
options:["<ul>","<ol>","<list>"],
answer:0
},

{
question:"Which keyword defines a function in JavaScript?",
options:["function","method","define"],
answer:0
},

{
question:"Which operator is used for comparison?",
options:["==","=","+="],
answer:0
}

];


/* ============================
Simulate Async Quiz Loading
============================ */

function fetchQuizData(){

return new Promise(resolve=>{

setTimeout(()=>{

resolve(quizQuestions);

},1000);

});

}

async function loadQuiz(){

const data = await fetchQuizData();

renderQuiz(data);

}


/* ============================
Render Quiz Questions
============================ */

function renderQuiz(questions){

const container = document.getElementById("quiz");

if(!container) return;

container.innerHTML = "";

questions.forEach((q,index)=>{

let optionsHTML = q.options.map((opt,i)=>`

<label>
<input type="radio" name="q${index}" value="${i}" onchange="updateProgress()">
${opt.replace(/</g,"&lt;").replace(/>/g,"&gt;")}
</label>

`).join("");

container.innerHTML += `

<div class="question">

<h3>${index + 1}. ${q.question}</h3>

${optionsHTML}

</div>

`;

});

}


/* ============================
Update Progress
============================ */

function updateProgress(){

let total = quizQuestions.length;

let answered =
document.querySelectorAll("input[type='radio']:checked").length;

let progress = Math.round((answered / total) * 100);

const progressBar =
document.getElementById("quizProgressBar");

const counter =
document.getElementById("questionCounter");

if(progressBar)
progressBar.style.width = progress + "%";

if(counter)
counter.innerText =
"Answered " + answered + " of " + total + " questions";

}


/* ============================
Submit Quiz
============================ */

function submitQuiz(){

let score = 0;

for(let i=0;i<quizQuestions.length;i++){

let selected =
document.querySelector(`input[name="q${i}"]:checked`);

if(!selected){

alert("Please answer all questions before submitting.");

return;

}

if(parseInt(selected.value) === quizQuestions[i].answer){

score++;

}

}


/* ============================
Calculate Results
============================ */

let percentage =
calculatePercentage(score,quizQuestions.length);

let grade =
calculateGrade(percentage);


/* ============================
Feedback Message
============================ */

let feedback;

switch(grade){

case "A":
feedback = "Excellent performance!";
break;

case "B":
feedback = "Good job!";
break;

case "C":
feedback = "Needs improvement.";
break;

default:
feedback = "Failed. Try again!";

}


/* ============================
Store Result
============================ */

const result = {
score,
percentage,
grade
};

localStorage.setItem(
"quizResult",
JSON.stringify(result)
);


/* ============================
Display Result
============================ */

const resultDiv =
document.getElementById("result");

if(resultDiv){

resultDiv.innerHTML = `

<div class="card p-3 shadow">

<h3>Quiz Result</h3>

<p><strong>Score:</strong> ${score}/${quizQuestions.length}</p>

<p><strong>Percentage:</strong> ${percentage}%</p>

<p><strong>Grade:</strong> ${grade}</p>

<p><strong>Feedback:</strong> ${feedback}</p>

</div>

`;

}

}


/* ============================
Initialize Quiz
============================ */

if (typeof window !== "undefined") {

window.onload = loadQuiz;

}


/* ============================
Utility Functions
============================ */

function calculatePercentage(score,total){

return Math.round((score/total)*100);

}

function calculateGrade(percentage){

if(percentage>=80) return "A";
else if(percentage>=60) return "B";
else if(percentage>=40) return "C";
else return "F";

}

function isPass(grade){

return grade==="A" || grade==="B" || grade==="C";

}


/* ============================
Export For Jest
============================ */

if(typeof module!=="undefined"){

module.exports={
calculatePercentage,
calculateGrade,
isPass
};

}