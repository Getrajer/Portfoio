class UnionFind{
    constructor(){
        //Number of elements in this union find
        this.size = 0;

        //Tracks number of components
        this.number_components = 0;

        //Used to track the sizes of each ot the components
        this.components = [];

        //id[i] points to the parent of i, if id[i] = i then i is a root node
        this.id = [];
    }

    //Find component 'p' belongs to
    Find(p){

        //Find root of the component set
        let root = p;

        while(root != this.id[root]){
            root = this.id[root];
        }

        //Compress the path leading back to the root
        while(p != root){
            let next = this.id[p];
            this.id[p] = root;
            p = next;
        }

        return root;
    }

    //Append array of components to the uf
    AppendComponents(data){
        this.components = data;
        this.size = this.size + data.length;


        return this;
    }

    AppendSingleComponent(data){
        this.number_components++;
        this.size++;
        this.components.push(data);
        let i = this.size;
        this.id.push(parseInt(this.size - 1));
    }

    //Return whether or not elements 'p' and 'q' are in the same component set
    Connected(p, q){
        return this.Find(p) == this.Find(q);
    }

    //Return size of the component set 'p' belongs to
    ComponentSize(p){
        return this.components[this.Find(p)];
    }

    //Will return number of elements in this Union Find / Disjoint set
    Size(){
        return this.size;
    }

    //Returns the number of remaining components sets
    Components(){
        return this.number_components;
    }

    //Unify the components sets containing elements 'p' and 'q'
    Unify(p,q){

        let root1 = this.Find(p);
        let root2 = this.Find(q);

        //These elements are already in the same group
        if(root1 == root2){return;}

        //Merge two components/sets together
        //Merge smaller to the larger one
        if(this.components[root1] < this.components[root2]){
            this.components[root2] += this.components[root1];
            this.id[root1] = root2;
        }
        else{
            this.components[root1] += this.components[root2];
            this.id[root2] = root1;
        }

        this.number_components--;
    }
}

//All buttons variables
const add_component_btn = document.getElementById('add_component');
const unify_btn = document.getElementById('unify');
const contains_btn = document.getElementById('contains');
const clear_btn = document.getElementById('clear');
const to_string_btn = document.getElementById('toString');

//Declaring Union Find
let uf = new UnionFind();


let output_counter = 1;

//Data nodes
const queue_container = document.getElementById("dataStructureObjects");

//2nd panel
const output_window = document.getElementById("outputWindow");
const components_number = document.getElementById("components_number");
const data_size = document.getElementById("data_numbers");



//Load defaults function
const LoadDefaults = () =>{
    let array = [10,20,30,40,50,60,70,80,90,100];

    for(let i = 0; i < array.length; i++){
        uf.AppendSingleComponent(array[i]);
    }   
    uf.Unify(0,1);
    console.log(uf.components[uf.Find(1)]);
    console.log(uf);
}

LoadDefaults();