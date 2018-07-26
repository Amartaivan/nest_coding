#include <iostream>

using namespace std;

void inline print(int i, int j){
    cout << i + 1 << ' ' << j + 1 << endl;
}

int main(){

    int n, m, i, j, s;
    cin >> n >> m;
    int A[n][m], direction = 3;
    s = n * m;
    
    for (i = 0; i < n; i++)
        for (j = 0; j < m; j++)
            A[i][j] = 0;

    //First circle
    //Move down
    for (i = 0, j = 0; i < n && s > 0; i++){
        A[i][j] = 1;
        print(i, j);
        s--;
    }
    i--;
    //Move right
    for (j = 1; j < m && s > 0; j++){
        A[i][j] = 1;
        print(i, j);
        s--;
    }
    j--;
    //Move up
    for (i--; i >= 0 && s > 0; i--){
        A[i][j] = 1;
        print(i, j);
        s--;
    }
    i++;

    //Eat cheese
    while (s > 0){

        switch (direction){
            case 0:
                i++;
                while (A[i][j] == 0 && s > 0){
                    A[i][j] = 1;
                    print(i, j);
                    s--;
                    i++;
                }
                i--;
                direction++;
                break;
            case 1:
                j++; 
                while (A[i][j] == 0 && s > 0){
                    A[i][j] = 1;
                    print(i, j);
                    s--;
                    j++;
                }
                j--;
                direction++;
                break;
            case 2:
                i--;
                while (A[i][j] == 0 && s > 0){
                    A[i][j] = 1;
                    print(i, j);
                    s--;
                    i--;
                }
                i++;
                direction++;
                break;
            case 3:
                j--;
                while (A[i][j] == 0 && s > 0){
                    A[i][j] = 1;
                    print(i, j);
                    s--;
                    j--;
                }
                j++;
                direction = 0;
                break;
        }
    }
    return 0;
}