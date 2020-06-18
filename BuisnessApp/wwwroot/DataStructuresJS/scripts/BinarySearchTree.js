class Node{
    constructor(){
        let Data;

        let Left;
        let Right;
    }
}

class BinarySearchTree{
    constructor(){
        
        //Tracks number of nodes
        let nodeCount = 0;

        //Root node of the tree
        let root_node = new Node();
    }

    //Will check if tree is empty
    IsEmpty(){
        return Size() === 0;
    }

    //Will return number of nodes
    Size(){
        return nodeCount;
    }

    //Will check if element already exist
    AddCheck(data){

        //If it includes element ignore it
        if(this.Contains(data)){
            return false;
        }
        else{
            this.root_node = this.Add(this.root_node, data);
            nodeCount++;
            return true;
        }
    }

    //Will add a value to binary tree
    Add(node, data){
        if(node == null){
            node = new Node();
            node.Data = data;
            node.Left = null;
            node.Right = null;
        }
        else{
            //Place lower elements values in left subtree
            if(data < node.Data){
                node.Left = this.AddCheck(node.Left, data);
            }else{
                node.Right = this.AddCheck(node.Right, data);
            }
        }

        return node;
    }


    //Remove a value from this binary tree, if it exists
    RemoveCheck(data){
        if(Contains(data)){
            this.root_node = this.Remove(this.root_node,data);
            nodeCount--;
            return true;
        }
        return false;
    }

    //Remove
    Remove(node, data){
        if(node == null) return null;

        let cmp = data;

        if(cmp < 0){
            node.Left = this.RemoveCheck(node.Right, data);
        }
        else if(cmp > 0){
            node.Right = this.RemoveCheck(node.Right, data);
        }
        else{
            if(node.Left == null){
                let rightChild = node.Right;

                node.data = null;
                node = null;

                return rightChild;
            }
            else if(node.Right == null){
                let leftChild = node.Left;

                node.data = null;
                node = null;

                return leftChild;
            }
            else{
                let tmp = this.digLeft(node.Right);

                node.data = tmp.data;

                node.Right = this.RemoveCheck(node.Right, tmp.data);
            }
        }

        return node;
    }

    digLeft(node){
        let cur = node;
        while(cur.Left != null){
            cur = cur.Right;
        }
        return cur;
    }

    digRight(node){
        let cur = node;
        while(cur.Right != null){
            cur = cur.Right;
        }
        return cur;
    }


    ContainsCheck(data){
        return Contains(this.root_node, data);
    }

    Contains(node, data){
        if(node == null) return false;

        let cmp = data;

        if(cmp < 0) return this.ContainsCheck(node.Left, data);

        if(cmp > 0) return this.ContainsCheck(node.Right, data);

        return true;
        
    }

    HeightCheck(){
        return this.Height(this.root_node);
    }


    Height(node){
        if(node == null) return 0;
        return Math.max( this.Height(node.Left), this.Height(node.Right)) + 1;
    }
    
}


let bst = new BinarySearchTree();

console.log(bst);

bst.AddCheck(10);
bst.AddCheck(20);
bst.AddCheck(30);
bst.AddCheck(40);
console.log(bst);
