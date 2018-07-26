#include <iostream>

using namespace std;

int main(){

    int n, k, i, j, len;
    cin >> n >> k;
    len = n;

    int A[n];
    for (i = 0; i < n; i++)
        A[i] = 0;

    for (len = n, i = k - 1; len > 1; len--){
        A[i % n] = 1;
        for (j = 0, i++; j < k; i++)
            if (A[i % n] != 1)
                j++;
        i--;
    }

    for (i = 0; A[i] != 0; i++);
    cout << i + 1 << endl;
    return 0;
}