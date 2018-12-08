#include <iostream>

using namespace std;

int main(){

    int n, tmp;
    cin >> n;
    int a[n];
 
    for (int i = 0; i < n; i++)
        cin >> a[i];

    for (int i = 1; i < n; i++){

        int number = a[i], j = i - 1;
        while (j >= 0 && a[j] > number){

            a[j + 1] = a[j];
            j--;
        } 
        a[j + 1] = number;
    }

    for (int b : a)
        cout << b << ' ';
    cout << endl;
    return 0;
}