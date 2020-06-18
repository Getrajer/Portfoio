//This class is an singly linked list is going to be used to create stack
class LinkedList{

    constructor(){
        this.head = null;
        this.tail = null;
        this.currentSize = 0;
    }

    //This function will add to the begin of the Linked List
    AddFirst(data){
        const new_Node = {data};
        new_Node.next = this.head;
        this.head = new_Node;

        if(this.currentSize === 0){
            this.tail = new_Node;
        }

        this.tail = this.tail;
        this.currentSize++;

        return this;
    }

    //This function will add to the end of the Linked List
    AddLast(data){
        const new_Node = {data};

        if(this.head === null){
            this.tail = new_Node;
            this.head = new_Node;
            this.currentSize++;
            return this;
        }
        new_Node.next = null;
        this.tail.next = new_Node;
        this.tail = new_Node;
        this.currentSize++;
        return this;
    }

    //This function inserts object on index position
    Insert(data, index){
        if(this.head === null){
            return null;
        }
        
        if(index > this.currentSize){
            return null;
        }

        if(index === 0){
            return this.AddFirst();
        }

        if(index === this.ListSize){
            return this.AddLast();
        }

        const new_Node = {data};
        let trav = this.head;
        let previous = null;
        let count = 0;

        while(count < index){
            previous = trav;
            trav = trav.next;
            count++;
            console.log(index + trav.data, count);
        }

        new_Node.next = previous.next;
        previous.next = new_Node;
        return this;
    }

    //This function will remove first item from the List
    RemoveFirst(){
        if(this.head === null){
            return null;
        }

        const removed_node = this.head;

        if(this.tail === this.head){
            this.head = null;
            this.tail = null;
        }
        else{
            this.head = this.head.next;
        }

        this.currentSize--;
        return removed_node;
    }

    //This function will clear list
    Clear(){
        let temp = this.head;
        while(temp != null){
            let next_node = temp.next;
            temp.next = null;
            temp = next_node;
        }
        this.currentSize = 0;
        this.head = null;
        this.tail = null;
    }

    //This function will remove Last item from the List
    RemoveLast(){
        if(this.head === null){
            return null;
        }

        if(this.head === this.tail){
            this.RemoveFirst();
        }

        let current = this.head;
        let previous = null;



        while(current !== this.tail){
            previous = current;
            current = current.next;

        }

        this.tail = previous;
        this.tail.next = null;
        this.currentSize--;
        return current;
    }

    //This function will remove specific object from the list
    Remove(data){
        if(this.head === null){
            return null;
        }
        console.log(data);

        if(this.head.data === data){
            return this.RemoveFirst();
        }

        if(this.tail.data === data){
            return this.RemoveLast();
        }

        let current = this.head;
        let previous = null;

        while(current !== null){
            if(current.data === data){
                previous.next = current.next;
                current.next = null;
                this.currentSize--;
                return current;
            }

            previous = current;
            current = current.next;
        }

        return null;
    }

    //Returns Size of the list
    ListSize(){
        return this.currentSize;
    }

    Contains(data){
        if(this.head === null){
            return false;
        }

        if(this.head.data === data){
            return true;
        }

        if(this.tail.data === data){
            return true;
        }
        console.log(data);
        let current = this.head;
        while(current != null){

            if(current.data == data){
                console.log(current.data == data);
                return true;
            }
            current = current.next;

            if(current === null){
                return false;
            }
        }
        return false;
    }

    //This function will show 1st object in the list
    PeekFirst(){
        if(this.head === null){
            return null;
        }
        return this.head;
    }

    //This function will show 1st object in the list
    PeekLast(){
        if(this.head === null){
            return null;
        }
        return this.tail;
    }

    //Will return true or false if list depending if list is empty
    IsEmpty(){
        return this.currentSize === 0;
    }

    IndexOf(data){
        if(this.head === null){
            return -1;
        }
        let iterator = 0;
        let test_node = this.head;

        while(test_node !== null){
            if(test_node.data === data){
                return iterator;
            }
            
            iterator++;
            test_node = test_node.next;
        }

        return -1;
    }

    //It will peek at index
    PeekAt(index){
        if(this.head === null){
            return null;
        }

        if(Number.isInteger(index)){

            if(index > this.currentSize){
                console.log("Index is bigger than size of the list");
                return null;
            }
            else
            {
                let counter = 0;
                let current = this.head;
                while(counter !== index){
                    current = current.next;
                    counter++;
                }
                console.log(current);
                return current;
            }
        }
        else
        {
            console.log("Index is not a number");
            return null;
        }
    }

    RemoveByIndex(index){
        if(this.head === null){
            return null;
        }

        if(Number.isInteger(index)){
            if(index > this.currentSize){
                console.log("Index is bigger than size of the list");
                return null;
            }
            else
            {

                if(index === 0){
                    return this.RemoveFirst();
                }

                let counter = 0;
                let current = this.head;
                let previous = null;
                while(counter !== index){
                    previous = current;
                    current = current.next;
                    counter++;
                }
                previous.next = current.next;
                current.next = null;
                this.currentSize--;
                return current;
            }
        }
        else
        {
            console.log("Index is not a number");
            return null;
        }
    }

    ToString(){
        if(this.head == null){
            return "";
        }

        let list_string = "[";
        let helping_node = this.head;

        while(helping_node !== null){
            if(helping_node !== this.tail){
                console.log(helping_node.data);
                list_string += helping_node.data + ", ";
                helping_node = helping_node.next;
            }
            else{
                list_string += helping_node.data;
                helping_node = helping_node.next;
            }
            
        }
        list_string += "]";
        return list_string;
    }

}


//Stack class
class Stack{

    constructor(){
        this.list = new LinkedList();
    }

    Push(data){
        return this.list.AddFirst(data);
    }

    Pop(){
        if(this.list.IsEmpty()){
            return null;
        }else{
            return this.list.RemoveFirst();
        }
    }

    PeekTop(){
        if(this.list.IsEmpty()){
            return null;
        }else{
            return this.list.PeekLast();
        }
    }

    PeekBottom(){
        if(this.list.IsEmpty()){
            return null;
        }else{
            return this.list.PeekFirst();
        }
    }

    Clear(){
        return this.list.Clear();
    }

    IsEmpty(){
        return this.list.IsEmpty();
    }

    Size(){
        return this.list.ListSize();
    }

    Contains(data){
        return this.list.Contains(data);
    }

    ToString(){
        return this.list.ToString();
    }

    PeekAt(index){
        return this.list.PeekAt(index);
    }
}


//All buttons variables
const push_btn = document.getElementById('push');
const pop_btn = document.getElementById('pop');
const contains_btn = document.getElementById('contains');
const peek_top_btn = document.getElementById('peekTop');
const peek_bottom_btn = document.getElementById('peekLast');
const clear_btn = document.getElementById('clear');
const to_string_btn = document.getElementById('toString');

//Stack variable
let stack = new Stack();

let output_counter = 1;

//Data nodes
const stack_container = document.getElementById("dataStructureObjects");

//2nd panel
const stack_size_display = document.getElementById("dataStructureSize");
const output_window = document.getElementById("outputWindow");
const head_display = document.getElementById("headDisplay");
const tail_display = document.getElementById("tailDisplay");

//Load defaults function
const LoadDefaults = () =>{
    stack.Push(10);
    stack.Push(20);
    stack.Push(30);
    stack.Push(40);
    stack.Push(50);
    stack.Push(60);
    stack.Push(70);
    stack.Push(80);
    stack.Push(90);
    stack.Push(100);


    let stack_size = stack.Size();
    let head = stack.PeekTop();
    let tail = stack.PeekBottom();

    if(stack_size !== 0){
        stack_size_display.innerHTML = stack_size;
        head_display.innerHTML = head.data;
        tail_display.innerHTML = tail.data;
    }
    else{
        stack_size_display.innerHTML = "Empty";
        head_display.innerHTML = "Empty";
        tail_display.innerHTML = "Empty";
    }

    for(let i = 0; i < stack_size; i++){
        let variable = stack.PeekAt(i);
        let next;

        if(variable.next == null){
            next = "Null";
        }
        else{
            next = variable.next.data;
        }

        let new_div = document.createElement('div');

        let div;

        if(i !== stack_size){
            div = '<div class="data_point">' +
                            '<div class="index">' +
                                '<p>' + i + '</p>' +
                            '</div>' +
                            '<div class="data_point_container">' +
                                '<p>' + variable.data + '</p>' + 
                                '<div class="small_underline"></div>' +
                                '<p class="small_p">Next: ' + next + '</p>' +
                            '</div>' +

                        '</div>';
        }

        new_div.innerHTML = div;

        stack_container.append(new_div);
        
    }

}

//Load defaults such as some sample data and set sizes and so on
LoadDefaults();


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

//This function is going to reset variables in (#linked_list_objects) div
const RefreshDom = () =>{
    
    let stack_size = stack.Size();
    let head = stack.PeekTop();
    let tail = stack.PeekBottom();

    if(stack_size !== 0){
        stack_size_display.innerHTML = stack_size;
        head_display.innerHTML = head.data;
        tail_display.innerHTML = tail.data;
    }
    else{
        stack_size_display.innerHTML = "Empty";
        head_display.innerHTML = "Empty";
        tail_display.innerHTML = "Empty";
    }

    let new_div = document.createElement('div');
    stack_container.innerHTML = "";
    
    if(stack_size != 0){
        stack_size_display.innerHTML = stack_size;
        stack_container.innerHTML = "";

        for(let i = 0; i < stack_size; i++){
            let variable = stack.PeekAt(i);
            let next;
    
            if(variable.next == null){
                next = "Null";
            }
            else{
                next = variable.next.data;
            }
    
            let new_div = document.createElement('div');
    
            let div;
    
            div = '<div class="data_point">' +
                            '<div class="index">' +
                                '<p>' + i + '</p>' +
                            '</div>' +
                            '<div class="data_point_container">' +
                                '<p>' + variable.data + '</p>' + 
                                '<div class="small_underline"></div>' +
                                '<p class="small_p">Next: ' + next + '</p>' +
                            '</div>' +

                        '</div>';
                        
            new_div.innerHTML = div;
            stack_container.append(new_div);
        }
    }
    else{
    }
    
    
}


//Crate new data point
const Push = () =>{
        let variable = document.getElementById("addInput").value;
        variable = parseInt(variable); 
        if(!isNaN(variable)){
            stack.Push(variable);
            document.getElementById("addInput").value = "";
            RefreshDom();
        }else{
            CreateOutput("It is not a number");
            document.getElementById("addInput").value = "";
        }
}
push_btn.addEventListener("click", Push);

//This function will pop element from begining of the stack
const Pop = () =>{
    let stack_size = stack.Size();
    if(stack_size != 0){
        let txt = stack.Pop();
        CreateOutput("Element: " + txt.data + " has been removed");
        RefreshDom();
    }
    else{
        CreateOutput("Stack is empty!");
    }
}
pop_btn.addEventListener("click", Pop);

//This function will check if stack contains variable
const Contains = () =>{
    let stack_size = stack.Size();
    if(stack_size != 0){
        let variable = document.getElementById("containsVar").value;
        variable = parseInt(variable); 

        let result = stack.Contains(variable);
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
        CreateOutput("Stack is empty!");
    }
}
contains_btn.addEventListener("click", Contains);

//This function will clear the list
const Clear = () =>{
    let stack_size = stack.Size();
    if(stack_size != 0){
        stack.Clear();
        CreateOutput("Stack has been cleared");
        RefreshDom();
    }
    else{
        CreateOutput("Stack is empty!");
    }
}
clear_btn.addEventListener("click", Clear);

//This function will print stack to the console
const ToString = () =>{
    let stack_size = stack.Size();
    if(stack_size != 0){
        let list_string = stack.ToString();
        CreateOutput(list_string);
    }
    else{
        CreateOutput("Stack is empty!");
    }
}
to_string_btn.addEventListener("click", ToString);

//This function will output top variable to the console
const Top = () =>{
    let stack_size = stack.Size();
    if(stack_size != 0){
        let top = stack.PeekTop();
        CreateOutput("Top is: " + top.data);
    }
    else{
        CreateOutput("Stack is empty!");
    }
}
peek_top_btn.addEventListener("click", Top);

//This function will output bottom variable to the console
const Bottom = () =>{
    let stack_size = stack.Size();
    if(stack_size != 0){
        let top = stack.PeekBottom();
        CreateOutput("Bottom is: " + top.data);
    }
    else{
        CreateOutput("Stack is empty!");
    }
}
peek_bottom_btn.addEventListener("click", Bottom);