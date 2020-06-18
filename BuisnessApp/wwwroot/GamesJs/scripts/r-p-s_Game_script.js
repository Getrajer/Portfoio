
//Declare variable for event-listener to 'play rock'
var rock;
rock = document.getElementById('rock');
var boolPlayRock = false;
//For AI 'rock' play
var boolAiRock = false;

//Declare variable for event-listener to 'play paper'
var paper;
paper = document.getElementById('paper');
var boolPlayPaper = false;
//For AI 'paper' play
var boolAiPaper = false;

//Declare variable for event-listener to 'play scissors'
var scissors;
scissors = document.getElementById('scissors');
var boolPlayScissors = false;
//For AI 'scissors' play
var boolAiScissors = false;

//Variable for score
var aiScore = 0;
var playerScore = 0;

//Score DOM elements
var player_Score_box;
player_Score_box = document.getElementById('player_score');

var ai_Score_box;
ai_Score_box = document.getElementById('ai_score');

//Declaring functions for game

//Function for Ai move
function AiMove(){

    //Random number will decide about Ai move
    var AIChoice = Math.floor(Math.random() * 3); 

    const myNode = document.getElementById("computer-field");
            while (myNode.firstChild) {
            myNode.removeChild(myNode.lastChild);
            }

    //This switch function will simulate Ai move 
    switch(AIChoice){
        case 0:{
            var oImg = document.createElement("img");
            oImg.setAttribute('src', '../images/r-p-s/rock.png');
            oImg.setAttribute('alt', 'rock-image');
            oImg.className = "card_img";
            document.getElementById('computer-field').appendChild(oImg);
            
            boolAiRock = true;
            break;
        }
        case 1:{
            var oImg = document.createElement("img");
            oImg.setAttribute('src', '../images/r-p-s/paper.png');
            oImg.setAttribute('alt', 'rock-image');
            oImg.className = "card_img";
            document.getElementById('computer-field').appendChild(oImg);

            boolAiPaper = true;
            break;

        }
        case 2:{
            var oImg = document.createElement("img");
            oImg.setAttribute('src', '../images/r-p-s/scissors.png');
            oImg.setAttribute('alt', 'rock-image');
            oImg.className = "card_img";
            document.getElementById('computer-field').appendChild(oImg);

            boolAiScissors = true;
            break;
        }
    }
}



//Function for 'playing rock'
function playRock(){
    ResetVariables();

    const myNode = document.getElementById("player-field");
    while (myNode.firstChild) {
        myNode.removeChild(myNode.lastChild);
    }
    boolPlayRock = true;

    var oImg = document.createElement("img");
    oImg.setAttribute('src', '../images/r-p-s/rock.png');
    oImg.setAttribute('alt', 'rock-image');
    oImg.className = "card_img";
    document.getElementById('player-field').appendChild(oImg);

    AiMove();
    R_P_S_Result();
    UpdateScore();
}

rock.addEventListener('click', playRock, false);


//Function for 'playing paper'
function playPaper(){
    ResetVariables();

    const myNode = document.getElementById("player-field");
    while (myNode.firstChild) {
        myNode.removeChild(myNode.lastChild);
    }
    boolPlayPaper = true;

    var oImg = document.createElement("img");
    oImg.setAttribute('src', '../images/r-p-s/paper.png');
    oImg.setAttribute('alt', 'rock-image');
    oImg.className = "card_img";
    document.getElementById('player-field').appendChild(oImg);

    AiMove();
    R_P_S_Result();
    UpdateScore();
}

paper.addEventListener('click', playPaper, false);


//Function for 'playing scissors'
function playScissors(){
    ResetVariables();

    const myNode = document.getElementById("player-field");
    while (myNode.firstChild) {
        myNode.removeChild(myNode.lastChild);
    }
    
    boolPlayScissors = true;

    var oImg = document.createElement("img");
    oImg.setAttribute('src', '../images/r-p-s/scissors.png');
    oImg.setAttribute('alt', 'rock-image');
    oImg.className = "card_img";
    document.getElementById('player-field').appendChild(oImg);

    AiMove();
    R_P_S_Result();
    UpdateScore();
}

scissors.addEventListener('click', playScissors, false);


//Function for result
function R_P_S_Result(){
    var resultContainer = document.getElementById('ResultContainer');
    resultContainer.innerHTML = "";

    if(boolPlayRock === true){
        if(boolAiRock === true){
            resultContainer.innerHTML = "Tie";
        }
        else if(boolAiPaper === true){
            resultContainer.innerHTML = "Ai won";
            aiScore++;
        }
        else if(boolAiScissors === true){
            resultContainer.innerHTML = "You won";
            playerScore++;
        }
    }
    else if(boolPlayPaper === true){
        if(boolAiRock === true){
            resultContainer.innerHTML = "You won";
            playerScore++;
        }
        else if(boolAiPaper === true){
            resultContainer.innerHTML = "Tie";

        }
        else if(boolAiScissors === true){
            resultContainer.innerHTML = "Ai won";
            aiScore++;
        }
    }
    else if(boolPlayScissors === true){
        if(boolAiRock === true){
            resultContainer.innerHTML = "Ai won";
            aiScore++;
        }
        else if(boolAiPaper === true){
            resultContainer.innerHTML = "You won";
            playerScore++;
        }
        else if(boolAiScissors === true){
            resultContainer.innerHTML = "Tie";

        }
    }
}

//Resets variables every time play function is called
function ResetVariables(){
    boolPlayScissors = false;
    boolPlayRock = false;
    boolPlayPaper = false;

    boolAiPaper = false;
    boolAiRock = false;
    boolAiScissors = false;
}


//Function which updates score
function UpdateScore(){
    player_Score_box.innerHTML = "Player score:" + playerScore;
    ai_Score_box.innerHTML = "AI score:" + aiScore;
}

function ShowLabels(){
    player_Score_box.innerHTML = "Player score:";
    ai_Score_box.innerHTML = "AI score:";
}
screen.orientation.lock('landscape');
ShowLabels();
