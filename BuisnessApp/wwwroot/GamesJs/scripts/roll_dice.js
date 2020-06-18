//Declare Classes
class Dice{
    constructor(id, name, points, reroll) {
        this.Id = id;
        this.Name = name;
        this.Points = points;
        this.reroll = reroll;
    }
}

class Player{
    constructor(name, win){
        this.Name = name;
        this.Wins = win;
    }
}
////

//Player 1 object
let player_1 = new Player("Player 1", 0);
//Player 2 object
let player_2 = new Player("Player 2", 0);

//Dices for player 1
const player_1_dices = 
[
    new Dice(0, "u_1", 0, true),
    new Dice(1, "u_2", 0, true),
    new Dice(2, "u_3", 0, true),
    new Dice(3, "u_4", 0, true),
    new Dice(4, "u_5", 0, true),
]

//Dices for player 2
const player_2_dices = 
[
    new Dice(0, "u_1", 0, true),
    new Dice(1, "u_2", 0, true),
    new Dice(2, "u_3", 0, true),
    new Dice(3, "u_4", 0, true),
    new Dice(4, "u_5", 0, true),
]


//Addresses for dice svg graphics
const dice_addresses = 
['url("../images/roll_dice/SVG/dice_1.svg")',
'url("../images/roll_dice/SVG/dice_2.svg")',
'url("../images/roll_dice/SVG/dice_3.svg")',
'url("../images/roll_dice/SVG/dice_4.svg")',
'url("../images/roll_dice/SVG/dice_5.svg")',
'url("../images/roll_dice/SVG/dice_6.svg")'];


//Get objects from the DOM
const button_1 = document.getElementById("button_1");

const alert_box = document.getElementById("alert_box");

const test = document.getElementById("test");

//Variables for dices rolling for player 1
const dice_1_player_1_roller = document.getElementById("dice_1_play");
const dice_2_player_1_roller = document.getElementById("dice_2_play");
const dice_3_player_1_roller = document.getElementById("dice_3_play");
const dice_4_player_1_roller = document.getElementById("dice_4_play");
const dice_5_player_1_roller = document.getElementById("dice_5_play");

//Variables for dices as score for player 1
const dice_1_player_1_score = document.getElementById("dice_1_user");
const dice_2_player_1_score = document.getElementById("dice_2_user");
const dice_3_player_1_score = document.getElementById("dice_3_user");
const dice_4_player_1_score = document.getElementById("dice_4_user");
const dice_5_player_1_score = document.getElementById("dice_5_user");

//Set bool variables
let roll_player_1_count = 0;

let player_1_move = true;
let player_2_move = false;

//This function will allow user one to choose which dices to reroll after 1st roll
const ChooseReroll_1 = (Dice) =>{
    if(roll_player_1_count === 1){
        if(Dice.reroll === false){
            player_1_dices[Dice.Id].reroll = true;
            //Highlight which dice you player want to change with border
            switch(Dice.Id){
                case 0: dice_1_player_1_score.style.border = "4px solid rgb(87, 0, 134)"; break;
                case 1: dice_2_player_1_score.style.border = "4px solid rgb(87, 0, 134)"; break;
                case 2: dice_3_player_1_score.style.border = "4px solid rgb(87, 0, 134)"; break;
                case 3: dice_4_player_1_score.style.border = "4px solid rgb(87, 0, 134)"; break;
                case 4: dice_5_player_1_score.style.border = "4px solid rgb(87, 0, 134)"; break;
            }
        }else{
            player_1_dices[Dice.Id].reroll = false;
            switch(Dice.Id){
                case 0: dice_1_player_1_score.style.border = "unset"; break;
                case 1: dice_2_player_1_score.style.border = "unset"; break;
                case 2: dice_3_player_1_score.style.border = "unset"; break;
                case 3: dice_4_player_1_score.style.border = "unset"; break;
                case 4: dice_5_player_1_score.style.border = "unset"; break;
            }
        }
    }
}

//This function will the be user one roll
const FirstRollU1 = () => {
    if(player_1_move == true){
        //Play until 2 turns
        if(roll_player_1_count !== 2){
            let counter = 0;
            if(player_1_dices[0].reroll == true){
                dice_1_player_1_score.style.content = "none";
            }
            if(player_1_dices[1].reroll == true){
                dice_2_player_1_score.style.content = "none";
            }
            if(player_1_dices[2].reroll == true){
                dice_3_player_1_score.style.content = "none";
            }
            if(player_1_dices[3].reroll == true){
                dice_4_player_1_score.style.content = "none";
            }
            if(player_1_dices[4].reroll == true){
                dice_5_player_1_score.style.content = "none";
            }

            //Reset border
            dice_1_player_1_score.style.border = "unset";
            dice_2_player_1_score.style.border = "unset"; 
            dice_3_player_1_score.style.border = "unset"; 
            dice_4_player_1_score.style.border = "unset"; 
            dice_5_player_1_score.style.border = "unset"; 

            for(let i = 0; i < 10; i++){
                AnimationRoll(i * 100, i);
                counter++;
                if(counter == 10){
                    i = counter;
                }
            }

            setTimeout(() => {
                for(let i = 0; i < player_1_dices.length; i++){
                    player_1_dices[i].reroll = false;
                    
                }
                roll_player_1_count++;
                dice_1_player_1_score.style.content = dice_addresses[(player_1_dices[0].Points - 1)];
                dice_2_player_1_score.style.content = dice_addresses[(player_1_dices[1].Points - 1)];
                dice_3_player_1_score.style.content = dice_addresses[(player_1_dices[2].Points - 1)];
                dice_4_player_1_score.style.content = dice_addresses[(player_1_dices[3].Points - 1)];
                dice_5_player_1_score.style.content = dice_addresses[(player_1_dices[4].Points - 1)];

                dice_1_player_1_roller.style.content = "none";
                dice_2_player_1_roller.style.content = "none";
                dice_3_player_1_roller.style.content = "none";
                dice_4_player_1_roller.style.content = "none";
                dice_5_player_1_roller.style.content = "none";

            }, 2000);
            if_user_1_first_roll = false;
            button_1.innerHTML = "Reroll";

            //Check state for player 1
            setTimeout(() => {
                CheckScorePlayer1();
            }, 2100);

            setTimeout(() => {
                alert_box.style.backgroundColor = "unset";
                alert_box.innerHTML = "";
            }, 3500);

            player_1_move = false;
            player_2_move = true;
            
        }
    }else{
        alert_box.innerHTML = "It is player 2 move!";
        setTimeout(() => {
            alert_box.style.backgroundColor = "unset";
            alert_box.innerHTML = "";
            GameStatus();
        }, 3500);
    }
}

//This function will do animation of rolling for the player 1
const AnimationRoll = (time = 100, i) =>{

    setTimeout(() => {
        if(player_1_dices[0].reroll == true){
            let score_u_1 = Math.floor(Math.random() * 6);  
            dice_1_player_1_roller.style.content = dice_addresses[score_u_1];

            //Set the score
            player_1_dices[0].Points = score_u_1 + 1;

            let top = Math.floor(Math.random() * 150);
            let left = Math.floor(Math.random() * 300) + 50;

            dice_1_player_1_roller.style.top = `${top}`;
            dice_1_player_1_roller.style.left = `${left}`;
        }

        if(player_1_dices[1].reroll == true){
            let score_u_1 = Math.floor(Math.random() * 6);  
            dice_2_player_1_roller.style.content = dice_addresses[score_u_1];

            //Set the score
            player_1_dices[1].Points = score_u_1 + 1;

            let top = Math.floor(Math.random() * 150);
            let left = Math.floor(Math.random() * 300) + 50;

            dice_2_player_1_roller.style.top = `${top}`;
            dice_2_player_1_roller.style.left = `${left}`;
        }

        if(player_1_dices[2].reroll == true){
            let score_u_1 = Math.floor(Math.random() * 6);  
            dice_3_player_1_roller.style.content = dice_addresses[score_u_1];

            //Set the score
            player_1_dices[2].Points = score_u_1 + 1;

            let top = Math.floor(Math.random() * 150);
            let left = Math.floor(Math.random() * 300) + 50;

            dice_3_player_1_roller.style.top = `${top}`;
            dice_3_player_1_roller.style.left = `${left}`;
        }

        if(player_1_dices[3].reroll == true){
            let score_u_1 = Math.floor(Math.random() * 6);  
            dice_4_player_1_roller.style.content = dice_addresses[score_u_1];

            //Set the score
            player_1_dices[3].Points = score_u_1 + 1;

            let top = Math.floor(Math.random() * 150);
            let left = Math.floor(Math.random() * 300) + 50;

            dice_4_player_1_roller.style.top = `${top}`;
            dice_4_player_1_roller.style.left = `${left}`;
        }

        if(player_1_dices[4].reroll == true){
            let score_u_1 = Math.floor(Math.random() * 6);  
            dice_5_player_1_roller.style.content = dice_addresses[score_u_1];

            //Set the score
            player_1_dices[4].Points = score_u_1 + 1;

            let top = Math.floor(Math.random() * 150);
            let left = Math.floor(Math.random() * 300) + 50;

            dice_5_player_1_roller.style.top = `${top}`;
            dice_5_player_1_roller.style.left = `${left}`;
        }

        
    }, time);
}


button_1.addEventListener("click", FirstRollU1);

dice_1_player_1_score.addEventListener("click", () => {ChooseReroll_1(player_1_dices[0]);});
dice_2_player_1_score.addEventListener("click", () => {ChooseReroll_1(player_1_dices[1]);});
dice_3_player_1_score.addEventListener("click", () => {ChooseReroll_1(player_1_dices[2]);});
dice_4_player_1_score.addEventListener("click", () => {ChooseReroll_1(player_1_dices[3]);});
dice_5_player_1_score.addEventListener("click", () => {ChooseReroll_1(player_1_dices[4]);});


//////////////////////////////////////////////////////////////////////////////////

//Player 2




let roll_player_2_count = 0;
let if_user_2_first_roll = true;
const button_2 = document.getElementById("button_2");



//Variables for dices rolling for player 2
const dice_1_player_2_roller = document.getElementById("dice_1_play_2");
const dice_2_player_2_roller = document.getElementById("dice_2_play_2");
const dice_3_player_2_roller = document.getElementById("dice_3_play_2");
const dice_4_player_2_roller = document.getElementById("dice_4_play_2");
const dice_5_player_2_roller = document.getElementById("dice_5_play_2");

//Variables for dices as score for player 2
const dice_1_player_2_score = document.getElementById("dice_1_user_2");
const dice_2_player_2_score = document.getElementById("dice_2_user_2");
const dice_3_player_2_score = document.getElementById("dice_3_user_2");
const dice_4_player_2_score = document.getElementById("dice_4_user_2");
const dice_5_player_2_score = document.getElementById("dice_5_user_2");


//This function will allow player 2 to choose which dices to reroll after 1st roll
const ChooseReroll_2 = (Dice) =>{
    if(roll_player_2_count === 1){
        if(Dice.reroll === false){
            player_2_dices[Dice.Id].reroll = true;
            //Highlight which dice you player want to change with border
            switch(Dice.Id){
                case 0: dice_1_player_2_score.style.border = "4px solid rgb(87, 0, 134)"; break;
                case 1: dice_2_player_2_score.style.border = "4px solid rgb(87, 0, 134)"; break;
                case 2: dice_3_player_2_score.style.border = "4px solid rgb(87, 0, 134)"; break;
                case 3: dice_4_player_2_score.style.border = "4px solid rgb(87, 0, 134)"; break;
                case 4: dice_5_player_2_score.style.border = "4px solid rgb(87, 0, 134)"; break;
            }
        }else{
            player_2_dices[Dice.Id].reroll = false;
            switch(Dice.Id){
                case 0: dice_1_player_2_score.style.border = "unset"; break;
                case 1: dice_2_player_2_score.style.border = "unset"; break;
                case 2: dice_3_player_2_score.style.border = "unset"; break;
                case 3: dice_4_player_2_score.style.border = "unset"; break;
                case 4: dice_5_player_2_score.style.border = "unset"; break;
            }
        }
    }
}


//This function will the be player 2 roll
const FirstRollU2 = () => {
    if(player_2_move === true){
        //Play until 2 turns
        if(roll_player_2_count !== 2){
            let counter = 0;
            if(player_2_dices[0].reroll == true){
                dice_1_player_2_score.style.content = "none";
            }
            if(player_2_dices[1].reroll == true){
                dice_2_player_2_score.style.content = "none";
            }
            if(player_2_dices[2].reroll == true){
                dice_3_player_2_score.style.content = "none";
            }
            if(player_2_dices[3].reroll == true){
                dice_4_player_2_score.style.content = "none";
            }
            if(player_2_dices[4].reroll == true){
                dice_5_player_2_score.style.content = "none";
            }

            //Reset border
            dice_1_player_2_score.style.border = "unset";
            dice_2_player_2_score.style.border = "unset"; 
            dice_3_player_2_score.style.border = "unset"; 
            dice_4_player_2_score.style.border = "unset"; 
            dice_5_player_2_score.style.border = "unset"; 

            for(let i = 0; i < 10; i++){
                AnimationRoll2(i * 100, i);
                counter++;
                if(counter == 10){
                    i = counter;
                }
            }

            setTimeout(() => {
                for(let i = 0; i < player_2_dices.length; i++){
                    player_2_dices[i].reroll = false;
                    
                }

                roll_player_2_count++;
                
                dice_1_player_2_score.style.content = dice_addresses[(player_2_dices[0].Points - 1)];
                dice_2_player_2_score.style.content = dice_addresses[(player_2_dices[1].Points - 1)];
                dice_3_player_2_score.style.content = dice_addresses[(player_2_dices[2].Points - 1)];
                dice_4_player_2_score.style.content = dice_addresses[(player_2_dices[3].Points - 1)];
                dice_5_player_2_score.style.content = dice_addresses[(player_2_dices[4].Points - 1)];

                dice_1_player_2_roller.style.content = "none";
                dice_2_player_2_roller.style.content = "none";
                dice_3_player_2_roller.style.content = "none";
                dice_4_player_2_roller.style.content = "none";
                dice_5_player_2_roller.style.content = "none";

            }, 2000);
            if_user_2_first_roll = false;
            button_2.innerHTML = "Reroll";


                    //Check state for player 1
            setTimeout(() => {
                CheckScorePlayer2();
            }, 2100);

            setTimeout(() => {
                alert_box.style.backgroundColor = "unset";
                alert_box.innerHTML = "";
            }, 3500);
        }
        player_1_move = true;
        player_2_move = false;

        
        if(roll_player_2_count === 2){
            GameStatus();
        }
    }else{
        alert_box.innerHTML = "It is player 1 move!";
        setTimeout(() => {
            alert_box.style.backgroundColor = "unset";
            alert_box.innerHTML = "";
        }, 1000);
    }

}



//This function will do animation of rolling for the player 2
const AnimationRoll2 = (time = 100, i) =>{

    setTimeout(() => {
        if(player_2_dices[0].reroll == true){
            let score_u_1 = Math.floor(Math.random() * 6);  
            dice_1_player_2_roller.style.content = dice_addresses[score_u_1];

            //Set the score
            player_2_dices[0].Points = score_u_1 + 1;

            let top = Math.floor(Math.random() * 150);
            let left = Math.floor(Math.random() * 300) + 50;

            dice_1_player_2_roller.style.top = `${top}`;
            dice_1_player_2_roller.style.left = `${left}`;
        }

        if(player_2_dices[1].reroll == true){
            let score_u_1 = Math.floor(Math.random() * 6);  
            dice_2_player_2_roller.style.content = dice_addresses[score_u_1];

            //Set the score
            player_2_dices[1].Points = score_u_1 + 1;

            let top = Math.floor(Math.random() * 150);
            let left = Math.floor(Math.random() * 300) + 50;

            dice_2_player_2_roller.style.top = `${top}`;
            dice_2_player_2_roller.style.left = `${left}`;
        }

        if(player_2_dices[2].reroll == true){
            let score_u_1 = Math.floor(Math.random() * 6);  
            dice_3_player_2_roller.style.content = dice_addresses[score_u_1];

            //Set the score
            player_2_dices[2].Points = score_u_1 + 1;

            let top = Math.floor(Math.random() * 150);
            let left = Math.floor(Math.random() * 300) + 50;

            dice_3_player_2_roller.style.top = `${top}`;
            dice_3_player_2_roller.style.left = `${left}`;
        }

        if(player_2_dices[3].reroll == true){
            let score_u_1 = Math.floor(Math.random() * 6);  
            dice_4_player_2_roller.style.content = dice_addresses[score_u_1];

            //Set the score
            player_2_dices[3].Points = score_u_1 + 1;

            let top = Math.floor(Math.random() * 150);
            let left = Math.floor(Math.random() * 300) + 50;

            dice_4_player_2_roller.style.top = `${top}`;
            dice_4_player_2_roller.style.left = `${left}`;
        }

        if(player_2_dices[4].reroll == true){
            let score_u_1 = Math.floor(Math.random() * 6);  
            dice_5_player_2_roller.style.content = dice_addresses[score_u_1];

            //Set the score
            player_2_dices[4].Points = score_u_1 + 1;

            let top = Math.floor(Math.random() * 150);
            let left = Math.floor(Math.random() * 300) + 50;

            dice_5_player_2_roller.style.top = `${top}`;
            dice_5_player_2_roller.style.left = `${left}`;
        }


        
    }, time);
}





dice_1_player_2_score.addEventListener("click", () => {ChooseReroll_2(player_2_dices[0]);});
dice_2_player_2_score.addEventListener("click", () => {ChooseReroll_2(player_2_dices[1]);});
dice_3_player_2_score.addEventListener("click", () => {ChooseReroll_2(player_2_dices[2]);});
dice_4_player_2_score.addEventListener("click", () => {ChooseReroll_2(player_2_dices[3]);});
dice_5_player_2_score.addEventListener("click", () => {ChooseReroll_2(player_2_dices[4]);});



////////Functionality for game play//////////


//Check state for player 1
let single_dices = [];
let double = [];
let triple = [];
let quatro = [];
let five = [];
let if_small_full = false;
let if_big_full = false;

const CheckScorePlayer1 = () =>{
    double = [];
    triple = [];
    quatro = [];
    five = [];
    single_dices = [];
    if_small_full = false;
    if_big_full = false;

    let player_check_dices = [];
    for(let i = 0; i < player_1_dices.length; i++){
        player_check_dices.push(player_1_dices[i]);
    }
    
    let dice = new Dice;

    for(let i = 0; i < player_check_dices.length; i++){
        let temp_array = [];
        temp_array.push(player_check_dices[i]);
        for(let j = 0; j < player_check_dices.length; j++){
            if(j !== i && (player_check_dices[j].Points - 1) === (player_check_dices[i].Points - 1)){
                temp_array.push(player_check_dices[j]);
                player_check_dices.splice(j, 1);
                j--;
            }
        }

        //Count single dices
        if(temp_array.length === 1){
            single_dices.push(temp_array[0]);
        }

        //Count pairs
        if(temp_array.length === 2){
            double.push(temp_array);
        }

        //Count 3 of same kind
        if(temp_array.length === 3){
            triple = temp_array;
        }

        //Count 4 of same kind
        if(temp_array.length === 4){
            quatro = temp_array;
        }

        if(temp_array.length === 5){
            let same_counter = 0;
            for(let i = 0; i < temp_array.length; i++){
                if(temp_array[0].Points === temp_array[i].Points){
                    same_counter = same_counter + 1;
                }
            }
            if(same_counter === 5){
                five = temp_array;
            }
        }
    }

    //Check if there is small full
    //Small full can happen only if it goes from top to bottom or from bottom to top
    //It needs to be as [1, 2, 3, 4, 5] in table
    if(single_dices.length === 5 && five.length === 0){
        let counter_full = 0;
        for(let i = 0; i < single_dices.length; i++){
            if(single_dices[i].Points == i){
                counter_full = counter_full + 1;
            }
        }

        if(counter_full === 5){
            //Up and down small full
            if_small_full = true;
        }
        
        if(counter_full < 5){
            counter_full = 0;
        }

        for(let i = single_dices.length - 1; i >= 0; i--){
            if(single_dices[i].Points == counter_full){
                counter_full = counter_full + 1;
            }
        }

        if(counter_full === 5){
            //Up and down small full
            if_small_full = true;
        }
    }

    //Check if this is big full which means points [2,3,4,5,6]
    if(if_small_full === false && single_dices.length === 5 && five.length === 0){
        let counter_full = 0;
        for(let i = 0; i < single_dices.length; i++){
            if(single_dices[i].Points == (i+1)){
                counter_full = counter_full + 1;
            }
        }

        if(counter_full === 5){
            //Up and down small full
            if_big_full_full = true;
        }
        
        if(counter_full < 5){
            counter_full = 0;
        }

        for(let i = single_dices.length - 1; i >= 0; i--){
            if(single_dices[i].Points == (counter_full + 1)){
                counter_full = counter_full + 1;
            }
        }

        if(counter_full === 5){
            //Up and down small full
            if_big_full = true;
        }
    }

    if(if_big_full === true){
        dice_1_player_1_score.style.border = "4px solid gold";
        dice_2_player_1_score.style.border = "4px solid gold";
        dice_3_player_1_score.style.border = "4px solid gold";
        dice_4_player_1_score.style.border = "4px solid gold";
        dice_5_player_1_score.style.border = "4px solid gold";
    }

    if(if_small_full === true){
        dice_1_player_1_score.style.border = "4px solid silver";
        dice_2_player_1_score.style.border = "4px solid silver";
        dice_3_player_1_score.style.border = "4px solid silver";
        dice_4_player_1_score.style.border = "4px solid silver";
        dice_5_player_1_score.style.border = "4px solid silver";


    }
    
    if(double.length > 0){
        for(let i = 0; i < double.length; i++){
            let x = double[i];
            switch(x[0].Id){
                case 0:{dice_1_player_1_score.style.border = "4px solid green"; break;}
                case 1:{dice_2_player_1_score.style.border = "4px solid green"; break;}
                case 2:{dice_3_player_1_score.style.border = "4px solid green"; break;}
                case 3:{dice_4_player_1_score.style.border = "4px solid green"; break;}
                case 4:{dice_5_player_1_score.style.border = "4px solid green"; break;}
            }
            switch(x[1].Id){
                case 0:{dice_1_player_1_score.style.border = "4px solid green"; break;}
                case 1:{dice_2_player_1_score.style.border = "4px solid green"; break;}
                case 2:{dice_3_player_1_score.style.border = "4px solid green"; break;}
                case 3:{dice_4_player_1_score.style.border = "4px solid green"; break;}
                case 4:{dice_5_player_1_score.style.border = "4px solid green"; break;}
            }
        }
    }

    if(triple.length > 0){
        for(let i = 0; i < triple.length; i++){
            let x = triple[i];
            switch(x.Id){
                case 0:{dice_1_player_1_score.style.border = "4px solid blue"; break;}
                case 1:{dice_2_player_1_score.style.border = "4px solid blue"; break;}
                case 2:{dice_3_player_1_score.style.border = "4px solid blue"; break;}
                case 3:{dice_4_player_1_score.style.border = "4px solid blue"; break;}
                case 4:{dice_5_player_1_score.style.border = "4px solid blue"; break;}
            }
        }
    }

    if(quatro.length > 0){
        for(let i = 0; i < quatro.length; i++){
            let x = quatro[i];
            switch(x.Id){
                case 0:{dice_1_player_1_score.style.border = "4px solid pink"; break;}
                case 1:{dice_2_player_1_score.style.border = "4px solid pink"; break;}
                case 2:{dice_3_player_1_score.style.border = "4px solid pink"; break;}
                case 3:{dice_4_player_1_score.style.border = "4px solid pink"; break;}
                case 4:{dice_5_player_1_score.style.border = "4px solid pink"; break;}
            }
        }
    }
    if(five.length > 0){
        for(let i = 0; i < five.length; i++){
            let x = five[i];
            switch(x.Id){
                case 0:{dice_1_player_1_score.style.border = "4px solid red"; break;}
                case 1:{dice_2_player_1_score.style.border = "4px solid red"; break;}
                case 2:{dice_3_player_1_score.style.border = "4px solid red"; break;}
                case 3:{dice_4_player_1_score.style.border = "4px solid red"; break;}
                case 4:{dice_5_player_1_score.style.border = "4px solid red"; break;}
            }
        }
    }

    //Show alert message
    if(if_small_full === true){
        alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
        alert_box.innerHTML = "<p>Player 1 scored small full</p>";
    }

    if(if_big_full === true){
        alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
        alert_box.innerHTML = "<p>Player 1 scored big full</p>";
    }

    if(triple.length > 0){
        alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
        alert_box.innerHTML = "<p>Player 1 scored triple</p>";
    }

    if(double.length > 0){
        alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
        alert_box.innerHTML = "<p>Player 1 scored double</p>";
    }

    if(triple.length > 0 && double.length > 0){
        alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
        alert_box.innerHTML = "<p>Player 1 scored triple and double</p>";
    }

    if(quatro.length > 0){
        alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
        alert_box.innerHTML = "<p>Player 1 scored quatro</p>";
    }

    if(five.length > 0){
        alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
        alert_box.innerHTML = "<p>Player 1 scored five</p>";
    }

}




//Check state for player 2
let single_dices_2 = [];
let double_2 = [];
let triple_2 = [];
let quatro_2 = [];
let five_2 = [];
let if_small_full_2 = false;
let if_big_full_2 = false;

const CheckScorePlayer2 = () =>{
    double_2 = [];
    triple_2 = [];
    quatro_2 = [];
    five_2 = [];
    single_dices_2 = [];
    if_small_full_2 = false;
    if_big_full_2 = false;

    let player_check_dices = [];
    for(let i = 0; i < player_2_dices.length; i++){
        player_check_dices.push(player_2_dices[i]);
    }
    
    let dice = new Dice;

    for(let i = 0; i < player_check_dices.length; i++){
        let temp_array = [];
        temp_array.push(player_check_dices[i]);
        for(let j = 0; j < player_check_dices.length; j++){
            if(j !== i && (player_check_dices[j].Points - 1) === (player_check_dices[i].Points) - 1){
                temp_array.push(player_check_dices[j]);
                player_check_dices.splice(j, 1);
                j--;
            }
        }

        //Count single dices
        if(temp_array.length === 1){
            single_dices_2.push(temp_array[0]);
        }

        //Count pairs
        if(temp_array.length === 2){
            double_2.push(temp_array);
        }

        //Count 3 of same kind
        if(temp_array.length === 3){
            triple_2 = temp_array;
        }

        //Count 4 of same kind
        if(temp_array.length === 4){
            quatro_2 = temp_array;
        }

        if(temp_array.length === 5){
            let same_counter = 0;
            for(let i = 0; i < temp_array.length; i++){
                if(temp_array[0].Points === temp_array[i].Points){
                    same_counter = same_counter + 1;
                }
            }
            if(same_counter === 5){
                five_2 = temp_array;
            }
        }
    }

    //Check if there is small full
    //Small full can happen only if it goes from top to bottom or from bottom to top
    //It needs to be as [1, 2, 3, 4, 5] in table
    if(single_dices_2.length === 5){
        let counter_full = 0;
        for(let i = 0; i < single_dices_2.length; i++){
            if(single_dices_2[i].Points == i){
                counter_full = counter_full + 1;
            }
        }

        if(counter_full === 5){
            //Up and down small full
            if_small_full_2 = true;
        }
        
        if(counter_full < 5){
            counter_full = 0;
        }

        for(let i = single_dices_2.length - 1; i >= 0; i--){
            if(single_dices_2[i].Points == counter_full){
                counter_full = counter_full + 1;
            }
        }

        if(counter_full === 5){
            //Up and down small full
            if_small_full_2 = true;
        }
    }

    //Check if this is big full which means points [2,3,4,5,6]
    if(if_small_full_2 === false && single_dices_2.length === 5){
        let counter_full = 0;
        for(let i = 0; i < single_dices_2.length; i++){
            if(single_dices_2[i].Points == (i+1)){
                counter_full = counter_full + 1;
            }
        }

        if(counter_full === 5){
            //Up and down small full
            if_big_full_full = true;
        }
        
        if(counter_full < 5){
            counter_full = 0;
        }

        for(let i = single_dices_2.length - 1; i >= 0; i--){
            if(single_dices_2[i].Points == (counter_full + 1)){
                counter_full = counter_full + 1;
            }
        }

        if(counter_full === 5){
            //Up and down small full
            if_big_full_2 = true;
        }
    }

    if(if_big_full_2 === true){
        dice_1_player_2_score.style.border = "4px solid gold";
        dice_2_player_2_score.style.border = "4px solid gold";
        dice_3_player_2_score.style.border = "4px solid gold";
        dice_4_player_2_score.style.border = "4px solid gold";
        dice_5_player_2_score.style.border = "4px solid gold";
    }

    if(if_small_full_2 === true){
        dice_1_player_2_score.style.border = "4px solid silver";
        dice_2_player_2_score.style.border = "4px solid silver";
        dice_3_player_2_score.style.border = "4px solid silver";
        dice_4_player_2_score.style.border = "4px solid silver";
        dice_5_player_2_score.style.border = "4px solid silver";


    }
    
    if(double_2.length > 0){
        for(let i = 0; i < double_2.length; i++){
            let x = double_2[i];
            switch(x[0].Id){
                case 0:{dice_1_player_2_score.style.border = "4px solid green"; break;}
                case 1:{dice_2_player_2_score.style.border = "4px solid green"; break;}
                case 2:{dice_3_player_2_score.style.border = "4px solid green"; break;}
                case 3:{dice_4_player_2_score.style.border = "4px solid green"; break;}
                case 4:{dice_5_player_2_score.style.border = "4px solid green"; break;}
            }
            switch(x[1].Id){
                case 0:{dice_1_player_2_score.style.border = "4px solid green"; break;}
                case 1:{dice_2_player_2_score.style.border = "4px solid green"; break;}
                case 2:{dice_3_player_2_score.style.border = "4px solid green"; break;}
                case 3:{dice_4_player_2_score.style.border = "4px solid green"; break;}
                case 4:{dice_5_player_2_score.style.border = "4px solid green"; break;}
            }
        }
    }

    if(triple_2.length > 0){
        for(let i = 0; i < triple_2.length; i++){
            let x = triple_2[i];
            switch(x.Id){
                case 0:{dice_1_player_2_score.style.border = "4px solid blue"; break;}
                case 1:{dice_2_player_2_score.style.border = "4px solid blue"; break;}
                case 2:{dice_3_player_2_score.style.border = "4px solid blue"; break;}
                case 3:{dice_4_player_2_score.style.border = "4px solid blue"; break;}
                case 4:{dice_5_player_2_score.style.border = "4px solid blue"; break;}
            }
        }
    }

    if(quatro_2.length > 0){
        for(let i = 0; i < quatro_2.length; i++){
            let x = quatro_2[i];
            switch(x.Id){
                case 0:{dice_1_player_2_score.style.border = "4px solid pink"; break;}
                case 1:{dice_2_player_2_score.style.border = "4px solid pink"; break;}
                case 2:{dice_3_player_2_score.style.border = "4px solid pink"; break;}
                case 3:{dice_4_player_2_score.style.border = "4px solid pink"; break;}
                case 4:{dice_5_player_2_score.style.border = "4px solid pink"; break;}
            }
        }
    }
    if(five_2.length > 0){
        for(let i = 0; i < five_2.length; i++){
            let x = five_2[i];
            switch(x.Id){
                case 0:{dice_1_player_2_score.style.border = "4px solid red"; break;}
                case 1:{dice_2_player_2_score.style.border = "4px solid red"; break;}
                case 2:{dice_3_player_2_score.style.border = "4px solid red"; break;}
                case 3:{dice_4_player_2_score.style.border = "4px solid red"; break;}
                case 4:{dice_5_player_2_score.style.border = "4px solid red"; break;}
            }
        }
    }

    //Show alert message
    if(if_small_full_2 === true){
        alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
        alert_box.innerHTML = "<p>Player 2 scored small full</p>";
    }

    if(if_big_full_2 === true){
        alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
        alert_box.innerHTML = "<p>Player 2 scored big full</p>";
    }

    if(triple_2.length > 0){
        alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
        alert_box.innerHTML = "<p>Player 2 scored triple</p>";
    }

    if(double_2.length > 0){
        alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
        alert_box.innerHTML = "<p>Player 2 scored double</p>";
    }

    if(triple_2.length > 0 && double.length > 0){
        alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
        alert_box.innerHTML = "<p>Player 2 scored triple and double</p>";
    }

    if(quatro_2.length > 0){
        alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
        alert_box.innerHTML = "<p>Player 2 scored quatro</p>";
    }

    if(five_2.length > 0){
        alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
        alert_box.innerHTML = "<p>Player 2 scored five</p>";
    }
}

let tie = false;
let p1_winner = true;
let p2_winner = false;
//Check winner
const CheckWinner = () => {


    //If both players has just 5 random numbers check which one has higher sum or of it is a tie
    if(single_dices.length === 5 && single_dices_2.length === 5)
    {
        let player1_score = 0;
        let player2_score = 0;

        for(let i = 0; i < single_dices_2.length; i++){
            player1_score = player1_score + single_dices[i].Points;
            player2_score = player2_score + single_dices_2[i].Points;
        }

        if(player2_score > player1_score){
            alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
            alert_box.innerHTML = "<p>Player 2 has won</p>";
            p2_winner = true;
        }
        if(player1_score > player2_score){
            alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
            alert_box.innerHTML = "<p>Player 1 has won</p>";
            p1_winner = true;
        }
        if(player1_score === player2_score){
            alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
            alert_box.innerHTML = "<p>Tie</p>";
            let tie = true;
        }
    }

    //If player 1 has double and player 2 nothing
    if(double.length > 0 && single_dices_2.length === 5){
        alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
        alert_box.innerHTML = "<p>Player 1 has won</p>";
        p1_winner = true;
    }

    //If player 2 has double and player 1 nothing
    if(double_2.length > 0 && single_dices.length === 5){
        alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
        alert_box.innerHTML = "<p>Player 2 has won</p>";
        p2_winner = true;
    }

    //If both players has double check which one is higher pointed
    if(double.length === 1 && double_2.length === 1 && triple.length === 0 && triple_2.length === 0){

        //If player 1 has bigger score on double
        if(double[0][0].Points > double_2[0][0].Points){
            alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
            alert_box.innerHTML = "<p>Player 1 has won</p>";
            p1_winner = true;
        }

        //If player 2 has bigger score on double
        if(double_2[0][0].Points > double[0][0].Points){
            alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
            alert_box.innerHTML = "<p>Player 2 has won</p>";
            p2_winner = true;
        }

        //If they got the same score the one with bigger number of score coming from free dices wins
        if(double_2[0][0].Points === double[0][0].Points){
            let player_1_score = 0;
            let player_2_score = 0;
            for(let i = 0; i < single_dices_2.length; i++){
                player_1_score = player_1_score + single_dices[i].Points;
                player_2_score = player_2_score + single_dices_2[i].Points;
            }

            if(player_1_score > player_2_score){
                alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
                alert_box.innerHTML = "<p>Player 1 has won</p>";
                p1_winner = true;
            }

            if(player_2_score > player_1_score){
                alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
                alert_box.innerHTML = "<p>Player 2 has won</p>";
                p2_winner = true;
            }

            if(player_1_score === player_2_score){
                alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
                alert_box.innerHTML = "<p>Tie</p>";
                tie = true;
            }
        }
    }

    // player 1 has 2 doubles and player 2 only one
    if(double.length === 2 && double_2.length === 1){
        alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
        alert_box.innerHTML = "<p>Player 1 has won</p>";
        p1_winner = true;
    }

    // player 2 has 2 doubles and player 1 only one
    if(double.length === 1 && double_2.length === 2){
        alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
        alert_box.innerHTML = "<p>Player 2 has won</p>";
        p2_winner = true;
    }

    //both of the players are having 2 doubles
    if(double.length === 2 && double_2.length === 2){
        let player_1_highers = 0;
        let player_2_highers = 0;

        if(double[0][0].Points > double_2[0][0].Points){
            player_1_highers++;
        }else if(double[0][0].Points < double_2[0][0].Points){
            player_2_highers++;
        }else if(double[0][0].Points === double_2[0][0].Points){
            console.log("Tie");
            tie = true;
            
        }

        if(double[0][0].Points > double_2[1][0].Points){
            player_1_highers++;
        }else if(double[0][0].Points < double_2[1][0].Points){
            player_2_highers++;
        }else if(double[0][0].Points === double_2[1][0].Points){
            console.log("Tie");
        }

        if(double[1][0].Points > double_2[0][0].Points){
            player_1_highers++;
        }else if(double[1][0].Points < double_2[0][0].Points){
            player_2_highers++;
        }else if(double[1][0].Points === double_2[0][0].Points){
            console.log("Tie");
        }
        
        if(double[1][0].Points > double_2[1][0].Points){
            player_1_highers++;
        }else if(double[1][0].Points < double_2[1][0].Points){
            player_2_highers++;
        }else if(double[1][0].Points === double_2[1][0].Points){
            console.log("Tie");
        }

        if(player_1_highers === player_2_highers){
            if(single_dices[0].Points > single_dices_2[0].Points){
                alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
                alert_box.innerHTML = "<p>Player 1 has won</p>";
                p1_winner = true;
                p2_winner = false;
            }
            else if(single_dices[0].Points < single_dices_2[0].Points){
                alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
                alert_box.innerHTML = "<p>Player 2 has won</p>";
                p2_winner = true;
                p1_winner = false;
            }
            else if(single_dices[0].Points === single_dices_2[0].Points){
                alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
                alert_box.innerHTML = "<p>Tie</p>";
                tie = true;
            }
        }

        if(player_1_highers > player_2_highers){
            alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
            alert_box.innerHTML = "<p>Player 1 has won</p>";
            p1_winner = true;
        }

        if(player_1_highers < player_2_highers){
            alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
            alert_box.innerHTML = "<p>Player 2 has won</p>";
            p2_winner = true;
        }
    }

    //If player 1 has triple and player 2 nothing
    if(triple.length === 3 && double.length === 0 && single_dices_2.length === 5){
        alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
        alert_box.innerHTML = "<p>Player 1 has won</p>";
        p2_winner = false;
        p1_winner = true;
    }

    //If player 1 has triple and double and player 2 nothing
    if(triple.length === 3 && double.length === 2 && single_dices_2.length === 5){
        alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
        alert_box.innerHTML = "<p>Player 1 has won</p>";
        p2_winner = false;
        p1_winner = true;
    }

    //If player 1 has triple and player 2 has double
    if(triple.length === 3 && single_dices.length === 2 && double_2.length === 1 && single_dices_2.length === 3){
        alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
        alert_box.innerHTML = "<p>Player 1 has won</p>";
        p2_winner = false;
        p1_winner = true;
    }

    //If player 1 has triple and player 2 has 2 double
    if(triple.length === 3 && single_dices.length === 2 && double_2.length === 2 && single_dices_2.length === 1){
        alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
        alert_box.innerHTML = "<p>Player 1 has won</p>";
        p2_winner = false;
        p1_winner = true;
    }

    //If player 2 has triple and player 1 nothing
    if(triple_2.length === 3 && double_2.length === 0 && single_dices.length === 5){
        alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
        alert_box.innerHTML = "<p>Player 2 has won</p>";
        p1_winner = false;
        p2_winner = true;
    }

    //If player 2 has triple and double and player 1 nothing
    if(triple_2.length === 3 && double_2.length === 2 && single_dices.length === 5){
        alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
        alert_box.innerHTML = "<p>Player 2 has won</p>";
        p1_winner = false;
        p2_winner = true;
    }

    //If player 2 has triple and player 1 has double
    if(triple_2.length === 3 && single_dices_2.length === 2 && double_2.length === 0 && single_dices.length === 3){
        alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
        alert_box.innerHTML = "<p>Player 2 has won</p>";
        p1_winner = false;
        p2_winner = true;
    }

    //If player 2 has triple and player 1 has 2 double
    if(triple_2.length === 3 && single_dices_2.length === 2 && double.length === 2 && single_dices.length === 1){
        alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
        alert_box.innerHTML = "<p>Player 2 has won</p>";
        p2_winner = true;
    }

    //If player 1 has triple and player 2 has triple
    if(triple.length === 3 && single_dices.length === 2 && triple_2.length === 3 && single_dices_2.length === 2){
        if(triple[0].Points > triple_2[0].Points){
            alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
            alert_box.innerHTML = "<p>Player 1 has won</p>";
            p1_winner = true;
        }

        if(triple[0].Points < triple_2[0].Points){
            alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
            alert_box.innerHTML = "<p>Player 2 has won</p>";
            p2_winner = true;
        }

        if(triple[0].Points === triple_2[0].Points){
            let player1_points = 0;
            let player2_points = 0;

            for(let i = 0; i < single_dices_2.length; i++){
                player1_points = player1_points + single_dices[i].Points;
                player2_points = player2_points + single_dices_2[i].Points;
            }

            if(player1_points > player2_points){
                alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
                alert_box.innerHTML = "<p>Player 1 has won</p>";
                p1_winner = true;
            }

            if(player1_points < player2_points){
                alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
                alert_box.innerHTML = "<p>Player 2 has won</p>";
                p2_winner = true;
            }

            if(player1_points === player2_points){
                alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
                alert_box.innerHTML = "<p>Tie</p>";
                tie = true;
                
            }
        }
    }

    //If player 1 has triple and double and player 2 has triple
    if(triple.length === 3 && double.length === 1 && triple_2.length === 3 && single_dices_2.length === 2){
        alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
        alert_box.innerHTML = "<p>Player 1 has won</p>";
        p1_winner = true;
    }

    //If player 1 has triple and double and player 2 has double
    if(triple.length === 3 && double.length === 1 && double_2.length === 1 && single_dices_2.length == 3){
        alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
        alert_box.innerHTML = "<p>Player 1 has won</p>";
        p1_winner = true;
    }

    //If player 1 has triple and double and player 2 has two doubles
    if(triple.length === 3 && double.length === 1 &&  double_2.length === 2){
        alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
        alert_box.innerHTML = "<p>Player 1 has won</p>";
        p1_winner = true;
    }

    //If player 2 has triple and double and player 1 has triple
    if(triple_2.length === 3 && double_2.length === 1 && triple.length === 3 && single_dices.length === 2){
        alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
        alert_box.innerHTML = "<p>Player 2 has won</p>";
        p2_winner = true;
    }

    //If player 2 has triple and double and player 1 has double
    if(triple_2.length === 3 && double_2.length === 1 && double.length === 1 && single_dices.length == 2){
        alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
        alert_box.innerHTML = "<p>Player 2 has won</p>";
        p2_winner = true;
    }

    //If player 2 has triple and double and player 1 has two doubles
    if(triple_2.length === 3 && double_2.length === 1 &&  double.length === 2 && single_dices.length == 1){
        alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
        alert_box.innerHTML = "<p>Player 2 has won</p>";
        p2_winner = true;
    }

    //If both players are having double and triple
    if(triple.length === 3 && double.length === 1  && triple_2.length === 3 && double_2.length === 1){
        console.log("this 0");
        //If player 1 has bigger triple
        if(triple[0].Points > triple_2[0].Points){
            alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
            alert_box.innerHTML = "<p>Player 1 has won</p>";
            p1_winner = true;
            p2_winner = false;
        }

        //If player 2 has bigger triple
        if(triple_2[0].Points > triple[0].Points){
            alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
            alert_box.innerHTML = "<p>Player 2 has won</p>";
            p2_winner = true;
            p1_winner = false;
        }

        //If both players are having equal triple 
        if(triple[0].Points === triple_2[0].Points){
            
            //but player 1 has bigger double
            if(double[0][0].Points > double_2[0][0].Points){
                alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
                alert_box.innerHTML = "<p>Player 1 has won</p>";
                p1_winner = true;
                p2_winner = false;
            }

            //but player 2 has bigger double
            if(double_2[0][0].Points > double[0][0].Points){
                alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
                alert_box.innerHTML = "<p>Player 2 has won</p>";
                p2_winner = true;
                p1_winner = false;
            }

            //Both are having same double
            if(double[0][0].Points === double_2[0][0].Points){
                alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
                alert_box.innerHTML = "<p>Tie</p>";
                tie = true;
                p1_winner = false;
                p2_winner = false;
                
            }
        }
    }

    //If player 1 has quatro but player 2 has nothing
    if(quatro.length === 4 && single_dices.length == 1 && single_dices.length === 1 && single_dices_2.length === 5){
        alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
        alert_box.innerHTML = "<p>Player 1 has won</p>";
        p1_winner = true;
        p2_winner = false;
    }

    //If player 1 has quatro but player 2 has double or 2x double 
    if(quatro.length === 4 && single_dices.length == 1 && double_2.length > 1 && single_dices_2.length === 1){
        alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
        alert_box.innerHTML = "<p>Player 1 has won</p>";
        p1_winner = true;
        p2_winner = false;
    }

    //If player 1 has quatro but player 2 has triple 
    if(quatro.length === 4 && single_dices.length == 1 && triple_2.length === 3 && single_dices_2.length === 2 ){
        alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
        alert_box.innerHTML = "<p>Player 1 has won</p>";
        p1_winner = true;
        p2_winner = false;
    }

    //If player 1 has quatro but player 2 has triple and double
    if(quatro.length === 4 && single_dices.length == 1 &&  triple_2.length === 3 && double_2.length === 1){
        alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
        alert_box.innerHTML = "<p>Player 1 has won</p>";
        p1_winner = true;
        p2_winner = false;
    }

    //If player 2 has quatro but player 1 has nothing
    if(quatro_2.length === 4 && single_dices.length == 1 && single_dices_2.length === 1 && single_dices.length === 5){
        alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
        alert_box.innerHTML = "<p>Player 2 has won</p>";
        p2_winner = true;
        p1_winner = false;
    }

    //If player 2 has quatro but player 1 has double or 2x double 
    if(quatro_2.length === 4 && single_dices_2.length == 1 && double.length > 1 && single_dices.length === 1){
        alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
        alert_box.innerHTML = "<p>Player 2 has won</p>";
        p2_winner = true;
        p1_winner = false;
    }

    //If player 1 has quatro but player 2 has triple 
    if(quatro_2.length === 4 && single_dices_2.length == 1 && triple.length === 3 && single_dices.length === 2 ){
        alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
        alert_box.innerHTML = "<p>Player 2 has won</p>";
        p2_winner = true;
        p1_winner = false;
    }

    //If player 2 has quatro but player 1 has triple and double
    if(quatro_2.length === 4 && single_dices_2.length == 1 && triple.length === 3 && double.length === 1){
        alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
        alert_box.innerHTML = "<p>Player 2 has won</p>";
        p2_winner = true;
        p1_winner = false;
    }

    //If both players are having quatro
    if(quatro.length === 4 && single_dices.length === 1 && quatro_2.length === 4 && single_dices_2.length === 1){
        
        //If players 1 quatro is having higher point
        if(quatro[0].Points > quatro_2[0].Points){
            alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
            alert_box.innerHTML = "<p>Player 1 has won</p>";
            p1_winner = true;
            p2_winner = false;
        }

        //If players 2 quatro is having higher point
        if(quatro_2[0].Points > quatro[0].Points){
            alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
            alert_box.innerHTML = "<p>Player 2 has won</p>";
            p2_winner = true;
            p1_winner = false;
        }

        //If players are having equal points
        if(quatro_2[0].Points === quatro[0].Points){
            //Player 1 has higher single dice
            if(single_dices[0].Points > single_dices_2[0].Points){
                alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
                alert_box.innerHTML = "<p>Player 1 has won</p>";
                p1_winner = true;
                p2_winner = false;
                tie = false;
            }

            //Player 2 has higher single dice
            if(single_dices_2[0].Points > single_dices[0].Points){
                alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
                alert_box.innerHTML = "<p>Player 2 has won</p>";
                p2_winner = true;
                p1_winner = false;
                tie = false;
            }

            //Players are having equal scores
            if(single_dices_2[0].Points === single_dices[0].Points){
                alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
                alert_box.innerHTML = "<p>Tie</p>";
                tie = true;
                p1_winner = false;
                p2_winner = false;
            }
        }
    }

    //If player 1 has five
    if(five.length === 5 && five_2.length === 0 && if_small_full_2 === false && if_big_full_2 === false){
        alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
        alert_box.innerHTML = "<p>Player 1 has won</p>";
        p1_winner = true;
        p2_winner = false;
        tie = false;
    }

    //If player 2 has five
    if(five_2.length === 5 && five.length === 0 && if_small_full === false && if_big_full === false){
        alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
        alert_box.innerHTML = "<p>Player 2 has won</p>";
        p2_winner = true;
        p1_winner = false;
        tie = false;
    }

    //If both players are having five
    if(five_2.length === 5 && five.length === 5){
        //But player 1 has higher point
        if(five[0].Points > five_2[0].Points){
            alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
            alert_box.innerHTML = "<p>Player 1 has won</p>";
            p1_winner = true;
            p2_winner = false;
            tie = false;
        }
        //But player 2 has higher point
        if(five_2[0].Points > five[0].Points){
            alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
            alert_box.innerHTML = "<p>Player 2 has won</p>";
            p2_winner = true;
            p1_winner = false;
            tie = false;
        }

        //Both player score the same points
        if(five_2[0].Points === five[0].Points){
            alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
            alert_box.innerHTML = "<p>Tie</p>";
            tie = true;
            p2_winner = false;
            p1_winner = false;
        }
    }

    //If player 1 has small full
    if(if_small_full === true && if_big_full_2 === false && if_small_full_2 === false){
        alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
        alert_box.innerHTML = "<p>Player 1 has won</p>";
        p1_winner = true;
        p2_winner = false;
    }

    //If player 1 has big full
    if(if_big_full === true && if_big_full_2 === false && if_small_full_2 === false){
        alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
        alert_box.innerHTML = "<p>Player 1 has won</p>";
        p1_winner = true;
        p2_winner = false;
    }


    //If player 2 has small full 
    if(if_small_full_2 === true && if_big_full === false && if_small_full === false){
        alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
        alert_box.innerHTML = "<p>Player 2 has won</p>";
        p2_winner = true;
        p1_winner = false;
    }

    //If player 2 has big full 
    if(if_big_full_2 === true && if_big_full === false && if_small_full === false){
        alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
        alert_box.innerHTML = "<p>Player 2 has won</p>";
        p2_winner = true;
        p1_winner = false;
    }

    //If both players are having small full
    if(if_small_full_2 === true && if_small_full === true){
        alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
        alert_box.innerHTML = "<p>Tie</p>";
        tie = true;
        p2_winner = false;
        p1_winner = false;
    }

    //If both players are having big full
    if(if_big_full_2 === true && if_big_full === true){
        alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
        alert_box.innerHTML = "<p>Tie</p>";
        tie = true;
        p2_winner = false;
        p1_winner = false;
    }

    if(p1_winner === true){
        player_1.Wins++;
    }

    if(p2_winner === true){
        player_2.Wins++;
    }

    if(tie === true){
    }
}

const score_p_1 = document.getElementById("score_1");
const score_p_2 = document.getElementById("score_2");


const GameStatus = () =>{
    FirstRollU2();

    setTimeout(() => {
        if(roll_player_2_count === 2){
            CheckWinner();


            setTimeout(() => {
                score_p_1.innerHTML = player_1.Wins;
                score_p_2.innerHTML = player_2.Wins;
            }, 50);
            
        }
    }, 3600);

    setTimeout(() => {
        if(roll_player_2_count === 2){
            alert_box.style.backgroundColor = "rgba(0, 0, 0, 0.527)";
            alert_box.innerHTML = "Next round";

            dice_1_player_1_score.style.content = dice_addresses[0];
            dice_2_player_1_score.style.content = dice_addresses[0]; 
            dice_3_player_1_score.style.content = dice_addresses[0];
            dice_4_player_1_score.style.content = dice_addresses[0]; 
            dice_5_player_1_score.style.content = dice_addresses[0];
            
            dice_1_player_1_score.style.border = "unset"
            dice_2_player_1_score.style.border = "unset"
            dice_3_player_1_score.style.border = "unset"
            dice_4_player_1_score.style.border = "unset"
            dice_5_player_1_score.style.border = "unset"

            dice_1_player_2_score.style.content = dice_addresses[0];
            dice_2_player_2_score.style.content = dice_addresses[0]; 
            dice_3_player_2_score.style.content = dice_addresses[0];
            dice_4_player_2_score.style.content = dice_addresses[0]; 
            dice_5_player_2_score.style.content = dice_addresses[0]; 

            dice_1_player_2_score.style.border = "unset"
            dice_2_player_2_score.style.border = "unset"
            dice_3_player_2_score.style.border = "unset"
            dice_4_player_2_score.style.border = "unset"
            dice_5_player_2_score.style.border = "unset"

            button_1.innerHTML = "Player 1 roll";
            button_2.innerHTML = "Player 2 roll";
            p1_winner = false;
            p2_winner = false
            tie = false;

            player_1_move = true;
            player_2_move = false;

            roll_player_1_count = 0;
            roll_player_2_count = 0;

            for(let i = 0; i < player_2_dices.length; i++){
                player_1_dices[i].reroll = true;
                player_2_dices[i].reroll = true;
            }

            setTimeout(() => {
        
                alert_box.style.backgroundColor = "transparent";
                alert_box.innerHTML = "";
                
            }, 1000);
        }
    }, 5600);



    
}

button_2.addEventListener("click", GameStatus);
