#include <iostream>
using namespace std;

int main(){

    int count = 0, n, max;

    cin >> n;
    int* a = new int[n];

    for (int i = 0; i < n; i++){
        cin >> a[i];
        if (i == 0 || a[i] > max)
            max = a[i];
    }

    for (int i = 0; i < n; i++)
        if (a[i] == max)
            count++;
    
    cout << count << endl;
    return 0;
}