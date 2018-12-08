#include <iostream>

using namespace std;

int binary_search(int* a, int l, int r, int x){
    
    while (l <= r){
        int m = (l + r) / 2;
        
        if (a[m] == x)
            return m;
        
        if (a[m] < x)
            l = m + 1;

        if (a[m] > x)
            r = m - 1;
    }

    return -1;
}

int main(){

    int n, k;
    cin >> n;
    int A[n];

    for (int i = 0; i < n; i++)
        cin >> A[i];
    cin >> k;

    cout << binary_search(A, 0, n - 1, k) << endl;
}