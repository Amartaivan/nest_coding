#include <iostream>
#include <string>

using namespace std;

int main(){

    int n;
    cin >> n;

    string result = "";
    while (n > 0){
        result += static_cast<char>((n & 1) + '0');
        n >>= 1;
    }
    result = string(result.rbegin(), result.rend());

    cout << result << endl;
    return 0;
}