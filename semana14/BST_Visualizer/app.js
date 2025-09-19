// Node class representing each node in the BST
class Node {
  constructor(value) {
    this.value = value;
    this.left = null;
    this.right = null;
  }
}

// Binary Search Tree class
class BST {
  constructor() {
    this.root = null;
  }

  // Insert a new node
  insert(value) {
    const newNode = new Node(value);
    if (this.root === null) {
      this.root = newNode;
    } else {
      this.insertNode(this.root, newNode);
    }
    this.render(); // Render the tree after insertion
  }

  // Helper function to insert a node
  insertNode(node, newNode) {
    if (newNode.value < node.value) {
      if (node.left === null) {
        node.left = newNode;
      } else {
        this.insertNode(node.left, newNode);
      }
    } else {
      if (node.right === null) {
        node.right = newNode;
      } else {
        this.insertNode(node.right, newNode);
      }
    }
  }

  // Traversal: Inorder
  inorder(node, result = []) {
    if (node !== null) {
      this.inorder(node.left, result);
      result.push(node.value);
      this.inorder(node.right, result);
    }
    return result;
  }

  // Traversal: Preorder
  preorder(node, result = []) {
    if (node !== null) {
      result.push(node.value);
      this.preorder(node.left, result);
      this.preorder(node.right, result);
    }
    return result;
  }

  // Traversal: Postorder
  postorder(node, result = []) {
    if (node !== null) {
      this.postorder(node.left, result);
      this.postorder(node.right, result);
      result.push(node.value);
    }
    return result;
  }

  // Search for a value and highlight the visited nodes
  searchNode(value) {
    let current = this.root;
    while (current !== null) {
      this.highlightNode(current); // Highlight current node
      if (value === current.value) {
        return current;
      } else if (value < current.value) {
        current = current.left;
      } else {
        current = current.right;
      }
    }
    return null;
  }

  // Delete a node from the tree
  deleteNode(value) {
    this.root = this.deleteRec(this.root, value);
    this.render(); // Re-render the tree after deletion
  }

  // Helper function to delete a node
  deleteRec(node, value) {
    if (node === null) {
      return node;
    }

    // Traverse the tree to find the node to delete
    if (value < node.value) {
      node.left = this.deleteRec(node.left, value);
    } else if (value > node.value) {
      node.right = this.deleteRec(node.right, value);
    } else {
      // Node to be deleted found

      // Case 1: No child (leaf node)
      if (node.left === null && node.right === null) {
        return null;
      }

      // Case 2: One child
      if (node.left === null) {
        return node.right;
      } else if (node.right === null) {
        return node.left;
      }

      // Case 3: Two children
      // Find the inorder successor (smallest node in the right subtree)
      const minNode = this.findMinNode(node.right);
      node.value = minNode.value;

      // Delete the inorder successor
      node.right = this.deleteRec(node.right, minNode.value);
    }

    return node;
  }

  // Helper function to find the minimum value node in a tree
  findMinNode(node) {
    while (node.left !== null) {
      node = node.left;
    }
    return node;
  }

  // Highlight node by adding a class
  highlightNode(node) {
    const nodeElement = document.getElementById(`node-${node.value}`);
    nodeElement.classList.add("highlight");
    setTimeout(() => {
      nodeElement.classList.remove("highlight");
    }, 1000); // Remove highlight after 1 second
  }

  // Render the tree as SVG
  render() {
    const canvas = document.getElementById("bstCanvas");
    canvas.innerHTML = ""; // Clear previous render

    if (this.root) {
      this.drawNode(this.root, 400, 50, 100); // Starting point (x=400, y=50)
    }
  }

  // Recursively draw nodes and edges
  drawNode(node, x, y, spacing) {
    const canvas = document.getElementById("bstCanvas");

    // Draw node (circle)
    const circle = document.createElementNS(
      "http://www.w3.org/2000/svg",
      "circle"
    );
    circle.setAttribute("cx", x);
    circle.setAttribute("cy", y);
    circle.setAttribute("r", 20);
    circle.setAttribute("id", `node-${node.value}`);
    canvas.appendChild(circle);

    // Draw value (text)
    const text = document.createElementNS("http://www.w3.org/2000/svg", "text");
    text.setAttribute("x", x);
    text.setAttribute("y", y + 5);
    text.textContent = node.value;
    canvas.appendChild(text);

    // Draw edges and child nodes
    if (node.left !== null) {
      this.drawEdge(x, y, x - spacing, y + 80); // Left child
      this.drawNode(node.left, x - spacing, y + 80, spacing / 2);
    }
    if (node.right !== null) {
      this.drawEdge(x, y, x + spacing, y + 80); // Right child
      this.drawNode(node.right, x + spacing, y + 80, spacing / 2);
    }
  }

  // Draw edges between nodes
  drawEdge(x1, y1, x2, y2) {
    const canvas = document.getElementById("bstCanvas");
    const line = document.createElementNS("http://www.w3.org/2000/svg", "line");
    line.setAttribute("x1", x1);
    line.setAttribute("y1", y1);
    line.setAttribute("x2", x2);
    line.setAttribute("y2", y2);
    line.setAttribute("stroke", "black");
    canvas.appendChild(line);
  }
}

// Instantiate a new BST
const bst = new BST();

// Insert a new node
function insertNode() {
  const value = parseInt(document.getElementById("inputValue").value);
  if (!isNaN(value)) {
    bst.insert(value);
  } else {
    alert("Please enter a valid number!");
  }
}

// Delete a node
function deleteNode() {
  const value = parseInt(document.getElementById("inputValue").value);
  if (!isNaN(value)) {
    bst.deleteNode(value);
  } else {
    alert("Please enter a valid number to delete!");
  }
}

// Search for a node and highlight the visited nodes
function searchNode() {
  const value = parseInt(document.getElementById("inputValue").value);
  if (!isNaN(value)) {
    bst.searchNode(value);
  } else {
    alert("Please enter a valid number to search!");
  }
}

// Show Inorder Traversal
function showInorder() {
  const result = bst.inorder(bst.root);
  document.getElementById(
    "traversalResult"
  ).textContent = `Inorder: ${result.join(", ")}`;
}

// Show Preorder Traversal
function showPreorder() {
  const result = bst.preorder(bst.root);
  document.getElementById(
    "traversalResult"
  ).textContent = `Preorder: ${result.join(", ")}`;
}

// Show Postorder Traversal
function showPostorder() {
  const result = bst.postorder(bst.root);
  document.getElementById(
    "traversalResult"
  ).textContent = `Postorder: ${result.join(", ")}`;
}
