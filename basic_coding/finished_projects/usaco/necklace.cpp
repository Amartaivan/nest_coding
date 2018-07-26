#include <iostream>
#include <vector>

using namespace std;

struct color{
    char color;
    int length;
};

int main(){

    int n, max = 1, maxi = 0, tmp1 = 0, result = 0;
    cin >> n;
    int size = n * 2;
    char A[size];
    color tmp;
    vector<color> B;

    cin >> A[0];
    tmp.color = A[0];
    tmp.length = 1;
    for (int i = 1; i < n; i++){
        cin >> A[i];

        if (A[i] == tmp.color)
            tmp.length++;
        else{
            B.push_back(tmp);
            tmp.color = A[i];
            tmp.length = 1;
        }
    }

    size = B.size() + 3;
    for (int i = 0; i < n && B.size() < size; i++)
        if (A[i] == tmp.color)
            tmp.length++;
        else{
            B.push_back(tmp);
            tmp.color = A[i];
            tmp.length = 1;
        }
    B.push_back(tmp);
    
    return 0;
}