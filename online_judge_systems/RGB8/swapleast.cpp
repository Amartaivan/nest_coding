#include <iostream>

using namespace std;

int main(){

    int n, i, min;
    cin >> n;

    int A[n];
    for (i = 0; i < n; i++)
        cin >> A[i];
	min = A[0];
	
    for (i = 0; i < n; i++)
        if (min > A[i])
        	min = A[i];
    
    for (i = 0; A[i] != min; i++);
    swap(A[i], A[0]);

    for (int a : A)
        cout << a << ' ';
    return 0;
}
