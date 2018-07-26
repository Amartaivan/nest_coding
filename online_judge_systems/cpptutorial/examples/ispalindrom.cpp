#include <iostream>

using namespace std;

int main(){

    int n = 5;
    char in_str[n + 1]; //null character
    cin.get(in_str, n + 1);

    bool result = true;
    for (int i = 0; i < n / 2; i++)  
        if (in_str[i] != in_str[n - 1 - i])
            result = false;

    if (result)
        cout << "YES" << endl;
    else
        cout << "NO" << endl;
    return 0;
}