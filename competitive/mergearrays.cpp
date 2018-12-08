#include <iostream>
#include <vector>

using namespace std;

int* merge_arrays(int A[], int B[], int n, int m){

    int* result = new int[n + m];
    int i = 0, j = 0;

    while (i < n && j < m){
        if (A[i] > B[j]){
            result[i + j] = B[j];
            j++;
        }
        else {
            result[i + j] = A[i];
            i++;
        }
    }
    if (i == n)
        while (j < m){
            result[i + j] = B[j];
            j++;
        }
    else
        while (i < n){
            result[i + j] = A[i];
            i++;
        }

    return result;
}
vector<int> merge(vector<int> A, vector<int> B){

    vector<int> result(A.size() + B.size());
    int i = 0, j = 0;

    while (i < A.size() && j < B.size()){
        if (A[i] > B[j]){
            result[i + j] = B[j];
            j++;
        }
        else {
            result[i + j] = A[i];
            i++;
        }
    }
    if (i == A.size())
        while (j < B.size()){
            result[i + j] = B[j];
            j++;
        }
    else
        while (i < A.size()){
            result[i + j] = A[i];
            i++;
        }

    return result;
}

int main(){

    int n, m;
    cin >> n;
    vector<int> A(n);

    for (int i = 0; i < n; i++)
        cin >> A[i];

    cin >> m;
    vector<int> B(m);
    for (int i = 0; i < m; i++)
        cin >> B[i];

    vector<int> result = merge(A, B);
    for (int i = 0; i < n + m; i++)
        cout << result[i] << ' ';
    cout << endl;
    return 0;
}