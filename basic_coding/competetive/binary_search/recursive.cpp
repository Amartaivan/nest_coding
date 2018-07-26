#include <iostream>

using namespace std;

int binary_search(int a[], int l, int r, int x){
    if (l <= r){

        int mid = (l + r) / 2;

        if (a[mid] == x)
            return mid;

        if (a[mid] > x)
            return binary_search(a, l, mid - 1, x);
        
        return binary_search(a, mid + 1, r, x);
    }

    return -1;
}