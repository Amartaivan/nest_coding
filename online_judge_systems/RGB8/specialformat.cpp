#include <iostream>
#include <vector>

using namespace std;

int main(){

    int buf = 1, num, i, tmp;
    vector<int> A;

    while (buf != 0){
        cin >> buf;
        num = 0;
        for (i = 0; i < buf; i++){
            cin >> tmp;
            num *= 10;
            num += tmp;
        }
        A.push_back(num);
    }
    A.pop_back();

    cout << A.size() << ' ';
    for (int temp : A)
        cout << temp << ' ';
    return 0;
}