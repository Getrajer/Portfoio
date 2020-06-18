//This class is an singly linked list implementaiton
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
        console.log(removed_node);
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
        console.log(current);
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
            console.log(current.data);
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
                console.log(iterator);
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


//Declaring all buttons
const add_first_btn = document.getElementById("addFirst");
const add_last_btn = document.getElementById("addLast");
const insert_at_btn = document.getElementById("insert");
const remove_value_btn = document.getElementById("removeValue");
const remove_by_index_btn = document.getElementById("removeIndex");
const contains_btn = document.getElementById("contains");
const remove_first_btn = document.getElementById("removeFirst");
const remove_last_btn = document.getElementById("removeLast");
const peek_top_btn = document.getElementById("peekTop");
const peek_last_btn = document.getElementById("peekLast");
const clear_btn = document.getElementById("clear");
const to_string_btn = document.getElementById("toString");

//Counter for notation in console
let output_counter = 1;

//Data nodes
const linked_list_objects_container = document.getElementById("dataStructureObjects");

//2nd panel
const list_size_display = document.getElementById("dataStructureSize");
const head_display = document.getElementById("headDisplay");
const tail_display = document.getElementById("tailDisplay");
const output_window = document.getElementById("outputWindow");

let linked_list = new LinkedList();

//Load defaults function
const LoadDefaults = () =>{
    linked_list.AddLast(10);
    linked_list.AddLast(20);
    linked_list.AddLast(30);
    linked_list.AddLast(40);
    linked_list.AddLast(50);
    linked_list.AddLast(60);
    linked_list.AddLast(70);
    linked_list.AddLast(80);
    linked_list.AddLast(90);
    linked_list.AddLast(100);


    let list_length = linked_list.ListSize();
    let head = linked_list.PeekFirst();
    let tail = linked_list.PeekLast();

    list_size_display.innerHTML = list_length;
    head_display.innerHTML = head.data;
    tail_display.innerHTML = tail.data;

    for(let i = 0; i < list_length; i++){
        let variable = linked_list.PeekAt(i);
        let next;

        if(variable.next == null){
            next = "Null";
        }
        else{
            next = variable.next.data;
        }

        let new_div = document.createElement('div');

        let div;

        if(i !== list_length){
            div = '<div class="data_point">' +
                            '<div class="data_point_container">' +
                                '<p>' + variable.data + '</p>' + 
                                '<div class="small_underline"></div>' +
                                '<p class="small_p">Next: ' + next + '</p>' +
                            '</div>' +
                            '<div class="index">' +
                                '<p>' + i + '</p>' +
                            '</div>' +
                        '</div>';
        }

        new_div.innerHTML = div;

        linked_list_objects_container.append(new_div);
        
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
const ReloadListDom = () =>{
    
    let list_length = linked_list.ListSize();
    let new_div = document.createElement('div');
    let head = linked_list.PeekFirst();
    let tail = linked_list.PeekLast();
    list_size_display.innerHTML = list_length;
    linked_list_objects_container.innerHTML = "";
    
    if(list_length != 0){
        list_size_display.innerHTML = list_length;
        head_display.innerHTML = head.data;
        tail_display.innerHTML = tail.data;
        linked_list_objects_container.innerHTML = "";

        for(let i = 0; i < list_length; i++){
            let variable = linked_list.PeekAt(i);
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
                            '<div class="data_point_container">' +
                                '<p>' + variable.data + '</p>' + 
                                '<div class="small_underline"></div>' +
                                '<p class="small_p">Next: ' + next + '</p>' +
                            '</div>' +
                            '<div class="index">' +
                                '<p>' + i + '</p>' +
                            '</div>' +
                        '</div>';
                        
            new_div.innerHTML = div;
            linked_list_objects_container.append(new_div);
        }
    }
    else{
        head_display.innerHTML = "Empty";
        tail_display.innerHTML = "Empty";
    }
    
    
}

//This function will add element on the begining
const AddFirstDom = () =>{
    let variable = document.getElementById("addInput").value;
    variable = parseInt(variable); 
    if(!isNaN(variable)){
        linked_list.AddFirst(variable);
        document.getElementById("addInput").value = "";
        ReloadListDom();
    }else{
        CreateOutput("It is not a number");
        document.getElementById("addInput").value = "";
    }
}
add_first_btn.addEventListener("click", AddFirstDom);

//This function will ad element at the end
const AddLastDom = () =>{
    let variable = document.getElementById("addInput").value;
    variable = parseInt(variable); 

    if(!isNaN(variable)){
        linked_list.AddLast(variable);
        document.getElementById("addInput").value = "";
        ReloadListDom();
    }else{
        CreateOutput("It is not a number");
        document.getElementById("addInput").value = "";
    }
}
add_last_btn.addEventListener("click", AddLastDom);

//This function will insert object on index
const InsertAt = () =>{
    let index = document.getElementById("insertIndex").value;
    let variable = document.getElementById("insertValue").value;

    variable = parseInt(variable);
    index = parseInt(index);

    if(!isNaN(variable) && !isNaN(index)){
        let listLength = linked_list.ListSize();
        if(listLength < index){
            CreateOutput("Index value is bigger than list size!");
            document.getElementById("insertValue").value = "";
            document.getElementById("insertIndex").value = "";
        }
        else{
            linked_list.Insert(variable, index);
            document.getElementById("addInput").value = "";
            document.getElementById("insertValue").value = "";
            document.getElementById("insertIndex").value = "";
            ReloadListDom();
        }
    }
    else{
        CreateOutput("Wrong index or input variable!");
        document.getElementById("insertValue").value = "";
        document.getElementById("insertIndex").value = "";
    }
}
insert_at_btn.addEventListener("click", InsertAt);

//Remove by data value
const RemoveByDataValue = () =>{
    let list_size = linked_list.ListSize();
    if(list_size != 0){
        let variable = document.getElementById("removeInput").value;
        variable = parseInt(variable); 
        let txt = linked_list.Remove(variable);
        CreateOutput("Element: " + txt.data + " has been removed");
        document.getElementById("removeInput").value = "";
        ReloadListDom();
    }
    else{
        CreateOutput("List is empty!");
        document.getElementById("removeInput").value = "";
    }
}
remove_value_btn.addEventListener("click", RemoveByDataValue);

//Remove by index
const RemoveByIndexDom = () =>{
    let list_size = linked_list.ListSize();
    if(list_size != 0){
        let variable = document.getElementById("removeInput").value;
        variable = parseInt(variable); 
        let list_length = linked_list.ListSize();

        if(variable > linked_list.ListSize){
            alert("Wrong index!");
            document.getElementById("removeInput").value = "";
            return;
        }

        if(variable < 0){
            alert("Wrong index!");
            document.getElementById("removeInput").value = "";
            return;
        }

        let txt = linked_list.RemoveByIndex(variable);
        CreateOutput("Element: " + txt.data + " has been removed");
        document.getElementById("removeInput").value = "";
        ReloadListDom();
    }else{
        CreateOutput("List is empty!");
        document.getElementById("removeInput").value = "";
    }
}
remove_by_index_btn.addEventListener("click", RemoveByIndexDom);

//This function will check if contains
const ContainsDom = () =>{

    let list_size = linked_list.ListSize();
    if(list_size != 0){
        let variable = document.getElementById("containsVar").value;
        variable = parseInt(variable); 
        let is_containing = linked_list.Contains(variable);

        if(is_containing){
            CreateOutput("Is containing");
        }
        else{
            CreateOutput("Is not containing");
        }
        document.getElementById("containsVar").value = "";
    }else{
        CreateOutput("List is empty!");
        document.getElementById("removeInput").value = "";
    }
}
contains_btn.addEventListener("click", ContainsDom);

//This function will remove first element from the list
const RemoveFirstDom = () =>{
    let list_size = linked_list.ListSize();
    if(list_size != 0){
        let txt = linked_list.RemoveFirst();
        CreateOutput("Element: " + txt.data + " has been removed");
        ReloadListDom();
    }else{
        CreateOutput("List is empty!");
        document.getElementById("removeInput").value = "";
    }
}
remove_first_btn.addEventListener("click", RemoveFirstDom);

//This function will remove last element from the list
const RemoveLastDom = () =>{
    let list_size = linked_list.ListSize();
    if(list_size != 0){
        let txt = linked_list.RemoveLast();
        CreateOutput("Element: " + txt.data + " has been removed");
        ReloadListDom();
    }else{
        CreateOutput("List is empty!");
        document.getElementById("removeInput").value = "";
    }
}
remove_last_btn.addEventListener("click", RemoveLastDom);

//This function will peek first element from the list
const PeekFirstDom = () =>{
    let list_size = linked_list.ListSize();
    if(list_size != 0){
        var first = linked_list.PeekFirst();
        first = first.data;
        CreateOutput("First item is: " + first);
    }else{
        CreateOutput("List is empty!");
        document.getElementById("removeInput").value = "";
    }

}
peek_top_btn.addEventListener("click", PeekFirstDom);

//This function will peek first element from the list
const PeekLastDom = () =>{
    let list_size = linked_list.ListSize();
    if(list_size != 0){
        var last = linked_list.PeekLast();
        last = last.data;
        CreateOutput("Last item is: " + last);
    }else{
        CreateOutput("List is empty!");
        document.getElementById("removeInput").value = "";
    }
}
peek_last_btn.addEventListener("click", PeekLastDom);

//This function will clear list
const ClearDom = () =>{
    let list_size = linked_list.ListSize();
    if(list_size != 0){
        linked_list.Clear();
        ReloadListDom();
        CreateOutput("List has been cleared");
    }else{
        CreateOutput("List is empty!");
        document.getElementById("removeInput").value = "";
    }
}
clear_btn.addEventListener("click", ClearDom);

//This function will write down whole list
const ToStringListDom = () =>{
    let list_size = linked_list.ListSize();
    if(list_size != 0){
        let list_str = linked_list.ToString();
        CreateOutput(list_str);
    }else{
        CreateOutput("List is empty!");
        document.getElementById("removeInput").value = "";
    }
}
to_string_btn.addEventListener("click", ToStringListDom);
