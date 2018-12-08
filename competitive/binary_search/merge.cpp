#include <iostream>
#include <vector>

using namespace std;

vector<int> merge(vector<int> A, vector<int> B){

    vector<int> result(A.size() + B.size());
    int i = 0, j = 0;

    while (i < A.size() && j < B.size()) {
        if (A[i] > B[j]){
            result[i + j] = B[j];
            j++;
        }
        else {
            result[i + j] = A[i];
            i++;
        }
    }
    if (i == A.size()) {
        while (j < B.size()){
            result[i + j] = B[j];
            j++;
        }
    } else {
        while (i < A.size()){
            result[i + j] = A[i];
            i++;
        }
    }

    return result;
}

int main(){

    int n, m;
    cin >> n >> m;
    vector<vector<int>> A(n, vector<int>(m));

    for (int i = 0; i < n; i++)
        for (int j = 0; j < m; j++)
            cin >> A[i][j];

    vector<int> result;
    for (int i = 0; i < n; i++)
        result = merge(result, A[i]);

    int k;
    cin >> k;

    cout << result[k - 1] << endl;
    return 0;
}