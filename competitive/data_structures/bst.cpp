#include <iostream>

using namespace std;

template <typename T>
class BSTnode {
public:
    T value;
    BSTnode<T> *left, *right;

    BSTnode(T val) : value(val) {
        left = nullptr;
        right = nullptr;
    };

    BSTnode* find(T val) {
        if (value == val)
            return this;

        if (val > value)
            if (right == nullptr)
                return nullptr;
            else
                return right->find(val);
        else
            if (left == nullptr)
                return nullptr;
            else
                return left->find(val);
    }
};

template <typename T>
class BinarySearchTree {
public:
    BSTnode<T> *root;

    void insert(T value, BSTNode<T> *__root = root) {
        if (value > __root->value)
            if (__root->right == NULL)
                __root->right = new BSTNode<T>(value);
            else
                insert(value, __root->right);
        else
            if (__root->left == NULL)
                __root->left = new BSTNode<T>(value);
            else
                insert(value, __root->left);
    }
};

int main() {
    return 0;
}