#include <iostream>
#include <string>

using namespace std;


string calculate(string in, int i, int j){
    string result = "";
    if (i == j){
        result = in[i];
        return result;
    }

    if (in[i] == in[j]){
        result += in[i];
        result += calculate(in, i + 1, j - 1);
        result += in[i];
    }
    else {
        string result_1 = calculate(in, i + 1, j);
        string result_2 = calculate(in, i, j - 1);

        if (result_1.length() < result_2.length()){
            result += in[i];
            result += result_1;
            result += in[i];
        }
        else{
            result += in[j];
            result += result_2;
            result += in[j];
        }
    }
    return result;
}

int main(){

    string input;
    cin >> input;

    string result = calculate(input, 0, input.length() - 1);
    cout << result.length() - input.length() << endl;
    cout << result << endl;
    return 0;
}