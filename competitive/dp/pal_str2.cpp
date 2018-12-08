#include <iostream>
#include <string>
#include <vector>

using namespace std;

int calculate(string in){
    int n = in.length();
    vector<vector<int>> dp(n, vector<int>(n, -1));

    for (int i = n - 2; i >= 0; i--){
        for (int j = i; j < n; j++){
            if (j == i)
                dp[i][j] = 0;
            else {
                if (in[i] == in[j])
                    if (j - i < 2)
                        dp[i][j] = 
            }
        }
    }
}

int main(){

    string input;
    cin >> input;

    cout << calculate(input) << endl;
    return 0;
}