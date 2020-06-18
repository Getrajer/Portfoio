class PriorityQueue{
    constructor(){
        //This array will store data of PQ
        this.array = [];

        //This variable will determine if PQ is from max to min or from min to max
        this.increasing = true;
    }

    //Will swim up new variable
    SwimUp(){
        let position = this.array.length - 1;
        while(position >= 0){
            let parent_node = this.array[parseInt((position - 1) / 2)];
            let temp = this.array[position];


            if(parent_node <= this.array[position]){break;}
            this.array[position] = parent_node;
            this.array[parseInt((position - 1) / 2)] = temp;
            position = parseInt((position - 1) / 2);
            parent_node = parseInt((position - 1) / 2);
            console.log(this.array);
        }
        return this;
    }

    //Will swim down new variable
    SwimDown(){
        let position = this.array.length - 1;
        while(position > 0){
            let temp = this.array[position];
            let parent_node = this.array[Math.floor((position - 1) / 2)];
    
            
            if(parent_node >= this.array[position]){break;}
            this.array[position] = parent_node;
            this.array[Math.floor((position - 1) / 2)] = temp;
            position = Math.floor((position - 1) / 2);
            console.log(this.array);
        }
        return this;
    }

    //Add element to the queue
    Enqueue(data){
        this.array.push(data);

        //When PQ is from min to max
        if(this.increasing == true){
            if(this.array.length != 1){
                this.SwimUp();
            }
        }//If PQ is from max to min
        else{
            if(this.array.length != 1){
                this.SwimDown();
            }
        }
    }

    //Will return top object
    PeekTop(){
    if(this.array.length == 0){return null;}        
        return this.array[0];
    }

    //Will return last object
    PeekLast(){
        if(this.array.length == 0){return null;}        
        return this.array[this.array.length - 1];
    }

    //Will return object at index in the list
    PeekAt(i){
        if(this.array.length == 0){return null;}        
        return this.array[i];
    }

    //Remove element from the top
    Dequeue(){
        if(this.array.length == 0){
            return null;
        }

        if(this.array.length == 1){
            let obj = this.array[0];
            this.array = [];
            return obj;
        }
        
        let top = this.array[0];
        let bottom = this.array[this.array.length - 1];
        this.array[0] = bottom;
        this.array[this.array.length - 1] = top;

        this.array.splice(this.array.length - 1, 1);

        console.log(this.array);
        if(this.increasing == true){
            this.SortMinMax(0);
            return top;
        }
        else{
            this.SortMaxMin(0);
            return top;
        }
    }

    //Will dequeue rest of data
    DequeueRest(){
        let dequeue_array = [];
        let queue_length = this.Size();
        for(let i = 0; i < queue_length; i++){
            dequeue_array.push(this.Dequeue());
        }

        return dequeue_array;
    }

    //This function will sort Queue from max to min
    SortMinMax(i){
        let index = i;
        let parent_node = this.array[index];
        let child_node_1 = this.array[Math.floor((2 * index) + 1)];
        let child_node_2 = this.array[Math.floor((2 * index) + 2)];

        if(child_node_2 == null){
            if(child_node_1 < parent_node){
                this.array[0] = child_node_1;
                this.array[1] = parent_node;
                return this;
            }
        }

        while(true){

            if(parent_node > child_node_1 || parent_node >= child_node_2){
                if(parent_node > child_node_1 && child_node_2 > child_node_1){
                    this.array[index] = child_node_1;
                    index = Math.floor((2 * index) + 1);
                    this.array[index] = parent_node;

                    child_node_1 = this.array[Math.floor((2 * index) + 1)];
                    child_node_2 = this.array[Math.floor((2 * index) + 2)];

                    if(child_node_1 == null){
                        break;
                    }

                    if(child_node_2 == null){
                        if(child_node_1 < parent_node){
                            this.array[index] = child_node_1;
                            this.array[Math.floor((2 * index) + 1)] = parent_node;
                            break;
                        }
                        else{
                            break;
                        }
                    }
                }
                else if(parent_node > child_node_1 && child_node_2 < child_node_1){
                    this.array[index] = child_node_2;
                    index = Math.floor((2 * index) + 2);
                    this.array[index] = parent_node;
                    
                    child_node_1 = this.array[Math.floor((2 * index) + 1)];
                    child_node_2 = this.array[Math.floor((2 * index) + 2)];

                    if(child_node_1 == null){
                        break;
                    }

                    if(child_node_2 == null){
                        if(child_node_1 < parent_node){
                            this.array[index] = child_node_1;
                            this.array[Math.floor((2 * index) + 1)] = parent_node;
                            break;
                        }
                        else{
                            break;
                        }
                    }
                }
                else{
                    break;
                }
            }
            else{
                break;
            }
        }
    }

    //This function will sort Queue from min to max
    SortMaxMin(i){
        let index = i;
        let parent_node = this.array[index];
        let child_node_1 = this.array[Math.floor((2 * index) + 1)];
        let child_node_2 = this.array[Math.floor((2 * index) + 2)];

        if(child_node_2 == null){
            if(child_node_1 > parent_node){
                this.array[0] = child_node_1;
                this.array[1] = parent_node;
                return this;
            }
        }

        while(true){
            //For testing
            //console.log("Child node 1: " + child_node_1 + " | Child node 2: " + child_node_2 + " | Parent node: " + parent_node);

            if(parent_node < child_node_1 || parent_node < child_node_2){
                if(parent_node < child_node_1 && child_node_2 < child_node_1){
                    this.array[index] = child_node_1;
                    index = Math.floor((2 * index) + 1);
                    this.array[index] = parent_node;

                    child_node_1 = this.array[Math.floor((2 * index) + 1)];
                    child_node_2 = this.array[Math.floor((2 * index) + 2)];

                    if(child_node_1 == null){
                        return this;
                    }

                    if(child_node_2 == null){
                        if(child_node_1 > parent_node){
                            this.array[index] = child_node_1;
                            this.array[Math.floor((2 * index) + 1)] = parent_node;
                            return this;
                        }
                        else{
                            return this;
                        }
                    }
                }
                else if(parent_node < child_node_1 && child_node_2 > child_node_1){
                    this.array[index] = child_node_2;
                    index = Math.floor((2 * index) + 2);
                    this.array[index] = parent_node;
                    
                    child_node_1 = this.array[Math.floor((2 * index) + 1)];
                    child_node_2 = this.array[Math.floor((2 * index) + 2)];

                    //For testing
                    //console.log("Child node 1: " + child_node_1 + " | Child node 2: " + child_node_2 + " | Parent node: " + parent_node);

                    if(child_node_1 == null){
                        return this;
                    }

                    if(child_node_2 == null){
                        if(child_node_1 > parent_node){
                            this.array[index] = child_node_1;
                            this.array[Math.floor((2 * index) + 1)] = parent_node;
                            return this;
                        }
                        else{
                            return this;
                        }
                    }
                }
                else{
                    return this;
                }
            }
            else{
                break;
            }
        }
    }

    //If array is empty
    IsEmpty(){
        return this.array.length == 0;
    }

    //Will return size
    Size(){
        return this.array.length;
    }

    //Will change order of data set
    ChangeOrder(){
        if(this.increasing === true){
            this.increasing = false;
            let new_set_array = this.array;
            let size_array = this.Size();
            this.array = [];
            for(let i = 0; i < size_array; i++){
                this.Enqueue(new_set_array[i]);
            } 
            return true;
        }
        else{
            this.increasing = true;
            let new_set_array = this.array;
            let size_array = this.Size();
            this.array = [];

            for(let i = 0; i < size_array; i++){
                this.Enqueue(new_set_array[i]);
            }

            return true;
        }
    }

    //Will clear array
    Clear(){
        this.array = [];
        return null;
    }

    //Will check if contains variable
    Contains(variable){
        if(this.array.length === 0){
            return false;
        }
        for(let i = 0; i < this.array.length; i++){
            if(this.array[i] === variable){
                return true;
            }
        }
        return false;
    }

    //will return objects from PQ in Dequeue order
    ToString(){
        if(this.array.length === 0){
            return null;
        }

        let return_array = this.array;

        //Return sorted in ascending order if PQ is set that way
        if(this.increasing === true){
            return_array.sort();
        }
        else{//Return sorted in descending order if PQ is set that way
            return_array.sort(function(a, b){return b-a});
        }
        
        return return_array;
    }
}

//All buttons variables
const enqueue_btn = document.getElementById('enqueue');
const dequeue_btn = document.getElementById('dequeue');
const dequeue_rest_btn = document.getElementById('dequeue_rest');
const change_order_btn = document.getElementById('change_order');
const contains_btn = document.getElementById('contains');
const peek_top_btn = document.getElementById('peekTop');
const peek_bottom_btn = document.getElementById('peekLast');
const clear_btn = document.getElementById('clear');
const to_string_btn = document.getElementById('toString');
const is_empty_btn = document.getElementById('is_empty');

//Counting the output in console
let output_counter = 1;

//Declaring PriorityQueue
let pq = new PriorityQueue();

//Data nodes
const PQ_container = document.getElementById("dataStructureObjects");

//2nd panel
const PQ_size_display = document.getElementById("dataStructureSize");
const output_window = document.getElementById("outputWindow");
const head_display = document.getElementById("headDisplay");
const tail_display = document.getElementById("tailDisplay");

//Create console output
const CreateOutput = (output_string) =>{
    if(output_string != null){
        console.log(output_string);
        let new_output = document.createElement('p');
        let text_node = document.createTextNode(output_counter + ". " + output_string);
        new_output.appendChild(text_node);
        output_window.append(new_output);
        output_counter++;
    }
}

//Load defaults function
const LoadDefaults = () =>{
    pq.Enqueue(10);
    pq.Enqueue(20);
    pq.Enqueue(30);
    pq.Enqueue(40);
    pq.Enqueue(50);
    pq.Enqueue(60);
    pq.Enqueue(70);
    pq.Enqueue(80);
    pq.Enqueue(90);
    pq.Enqueue(100);
    pq.Enqueue(110);
    pq.Enqueue(120);
    pq.Enqueue(130);
    pq.Enqueue(140);
    RefreshDom();
}

//Will refresh dom when changing something in the list
const RefreshDom = () =>{
    PQ_container.innerHTML = "";
    let list_length = pq.Size();
    let head = pq.PeekTop();
    let tail = pq.PeekLast();

    if(pq.array.length !== 0){
        PQ_size_display.innerHTML = list_length - 1;
        head_display.innerHTML = head;
        tail_display.innerHTML = tail;
    }
    else{
        PQ_size_display.innerHTML = "Unset";
        head_display.innerHTML = "Empty";
        tail_display.innerHTML = "Empty";
    }


    let queue_row_counter = 1;
    let queue_counter = 0;
    let queue_row_next = 1 * 2;
    let row_Id = ""

    let indexer = 0;
    let width_string = "100px";
    let width_value = 100;
    let padding = 20;
    let padding_string = "0px 20px"

    for(let i = 0; i < list_length; i++){
        let variable = pq.PeekAt(i);
        let next = pq.PeekAt(i + 1);
        let str_fr = "";
        indexer = i;
        if(queue_counter == 0){
            for(let j = 0; j < queue_row_counter; j++){
                str_fr += " 1fr ";
            }
            //Create Id for data point container
            row_Id = 'queue_row_' + queue_row_counter;
            let div_row = '<div id="' + row_Id + '" class="queue_row" style="grid-template-columns: ' + str_fr + ';">'
    
            //Create Container For data points
            let new_div_row = document.createElement('div');
            new_div_row.className = ("container_flex");
            let div_row_container;

            div_row_container =   div_row +
                    '</div>';

            new_div_row.innerHTML = div_row_container;
            PQ_container.append(new_div_row);
        }
        
        let row_div_pointer = document.getElementById(row_Id);
        let new_data_point = document.createElement('div');
        let div;

        let child_node_1 = pq.array[Math.floor((i * 2) + 1)];
        let child_node_2 = pq.array[Math.floor((i * 2) + 2)];

        if(child_node_1 == null){
            child_node_1 = "N";
        }
        else{
            
        }

        if(child_node_2 == null){
            child_node_2 = "N";
        }

        if(i !== list_length){
            div = '<div class="data_point" style="padding: ' + padding_string + ';">' +
                            '<div class="data_point_container">' +
                                '<p>' + variable + '</p>' + 
                                '<div class="small_underline"></div>' +
                                '<p class="small_p">L: ' + child_node_1 + ' | R: ' + child_node_2 + ' </p>' +
                            '</div>' +
                            '<div class="index">' +
                                '<p>' + indexer + '</p>' +
                            '</div>' +
                        '</div>';
        }
        new_data_point.innerHTML = div;
        row_div_pointer.append(new_data_point);

        //For testing
        //console.log("Queue next: " + queue_row_next + " Queue counter:" + queue_counter + " Queue row counter: " + queue_row_counter);

        queue_counter++;
        if(next != null){
            if(queue_row_counter === queue_counter){
                queue_row_counter = queue_row_next;
                queue_row_next = queue_row_next * 2;
                queue_counter = 0;
                width_value = width_value * 0.9;
                width_string = width_value + "px";
    
                padding = padding * 1.1;
                padding_string = '0px ' + padding + 'px';
            }
        }
    }
    //Create empty elements
    while(queue_row_counter != queue_counter){
        queue_counter++;
        indexer++;

        let row_div_pointer = document.getElementById(row_Id);
        let new_data_point = document.createElement('div');
        let div;
        div = '<div class="data_point" style="padding: ' + padding_string + ';">' +
                        '<div class="data_point_container">' +
                            '<p>' + 'Empty' + '</p>' + 
                        '</div>' +
                        '<div class="index">' +
                            '<p>' + indexer + '</p>' +
                        '</div>' +
                    '</div>';
        new_data_point.innerHTML = div;
        row_div_pointer.append(new_data_point);
    }
}

//Load defaults such as some sample data and set sizes and so on
LoadDefaults();

//Add Element to the PQ
const EnqueueDom = () =>{
    let variable = document.getElementById("addInput").value;
    variable = parseInt(variable); 
    if(!isNaN(variable)){
        pq.Enqueue(variable);
        document.getElementById("addInput").value = "";
        RefreshDom();
    }else{
        CreateOutput("It is not a number");
        document.getElementById("addInput").value = "";
    }
}
enqueue_btn.addEventListener("click", EnqueueDom);

//Function will remove element from PQ
const DequeueDom = () =>{
    let queue_size = pq.Size();
    if(queue_size != 0){
        let txt = pq.Dequeue();
        CreateOutput("Element: " + txt + " has been removed");
        RefreshDom();
    }
    else{
        CreateOutput("Queue is empty!");
    }
}
dequeue_btn.addEventListener("click", DequeueDom);

//Will dequeue rest of the elements from the PQ
const DequeueRestDom = () =>{
    let queue_size = pq.Size();
    if(queue_size != 0){
        let array_rest = pq.DequeueRest();
        CreateOutput(array_rest);
        RefreshDom();
    }
    else{
        CreateOutput("Queue is empty!");
    }
}
dequeue_rest_btn.addEventListener("click", DequeueRestDom);

//Will change order of the list either from min to max or max to min
const ChangeOrderDom = () =>{
    pq.ChangeOrder();
    CreateOutput("Order has been changed!");
    RefreshDom();
}
change_order_btn.addEventListener("click", ChangeOrderDom);

//This function will clear the list
const ClearDom = () =>{
    let queue_size = pq.Size();
    if(queue_size != 0){
        pq.Clear();
        CreateOutput("Queue has been cleared");
        RefreshDom();
    }
    else{
        CreateOutput("Queue is empty!");
    }
}
clear_btn.addEventListener("click", ClearDom);

//This function will output top variable to the console
const TopDom = () =>{
    let queue_size = pq.Size();
    if(queue_size != 0){
        let top = pq.PeekTop();
        CreateOutput("Top is: " + top);
    }
    else{
        CreateOutput("Queue is empty!");
    }
}
peek_top_btn.addEventListener("click", TopDom);

//This function will output bottom variable to the console
const BottomDom = () =>{
    let queue_size = pq.Size();
    if(queue_size != 0){
        let top = pq.PeekLast();
        CreateOutput("Bottom is: " + top);
    }
    else{
        CreateOutput("Queue is empty!");
    }
}
peek_bottom_btn.addEventListener("click", BottomDom);

//Will check if pq is empty
const IsEmptyDom = () =>{
    let isEmpty = pq.IsEmpty(); 

    if(isEmpty){CreateOutput("Is empty");}
    if(!isEmpty){CreateOutput("Is not empty");}
}
is_empty_btn.addEventListener("click", IsEmptyDom);

//If contains
const ContainsDom = () =>{
    let queue_size = pq.Size();
    if(queue_size != 0){
        let variable = document.getElementById("containsVar").value;
        variable = parseInt(variable); 

        let result = pq.Contains(variable);
        if(result == true){
            CreateOutput("Yes it contains");
        }
        else{
            CreateOutput("No it not contains");
        }

        document.getElementById("containsVar").value = "";
        RefreshDom();
    }
    else{
        CreateOutput("Queue is empty!");
    }
}
contains_btn.addEventListener("click", ContainsDom);

//Will print data in order to the console
const ToStringDom = () =>{
    if(pq.array.length !== 0){
        let return_array = pq.ToString();
        CreateOutput(return_array);
    }
    else{
        CreateOutput("Queue is empty!");
    }
}
to_string_btn.addEventListener("click", ToStringDom);