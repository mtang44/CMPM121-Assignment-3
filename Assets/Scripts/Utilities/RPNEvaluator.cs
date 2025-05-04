using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System;
using System.Linq;
#nullable enable

public class RPN
{
    public static int calculateRPN(string expression, Dictionary<string, int>? vars = null)
    {
        Stack<int> stack = new Stack<int>();
        Dictionary<string, int> variables = vars ?? new Dictionary<string,int>();
        // Dictionary<string, int> variables = new Dictionary<string, int>();
        string[] operations = {"+", "-", "*", "/", "%"};
        foreach (string token in expression.Split(" "))
        {
            if (variables != null && variables.ContainsKey(token))
                {stack.Push(variables[token]);}
            else if (operations.Contains(token)){
                int a = stack.Pop();
                int b = stack.Pop();
                stack.Push(applyOperator(token, b, a));
            } else {
                Debug.Log("trying to parse " + token);
                stack.Push(int.Parse(token));
            }
        }
        return stack.Pop();
    }

    public static float calculateRPNFloat(string expression, Dictionary<string, float>? vars = null) {
        Stack<float> stack = new Stack<float>();
        Dictionary<string, float> variables = vars ?? new Dictionary<string, float>();
        string[] operations = {"+", "-", "*", "/", "%"};
        foreach (string token in expression.Split(" "))
        {
            if (variables.ContainsKey(token))
                {stack.Push(variables[token]);}
            else if (operations.Contains(token)){
                float a = stack.Pop();
                float b = stack.Pop();
                stack.Push(applyOperatorFloat(token, b, a));
            } else {
                Debug.Log("trying to parse " + token);
                stack.Push(float.Parse(token));
            }
        }
        return stack.Pop();
    }
    public static float applyOperatorFloat(string token, float b, float a)
    {
        if(token == "+")
        {
            return b + a;
        }
        else if(token =="-" )
        {
            return b - a;
        } 
        else if(token == "*")
        {
            return b * a;
        }
        else if(token == "/")
        {
            return b / a;
        } 
        else if(token == "%")
        {
            return b % a;
        }
        else
        {
            Debug.Log($"not a valid operation token: {token}");
            return 0;
        }
    }
    public static int applyOperator(string token, int b, int a)
    {
        if(token == "+")
        {
            return b + a;
        }
        else if(token =="-" )
        {
            return b - a;
        } 
        else if(token == "*")
        {
            return b * a;
        }
        else if(token == "/")
        {
            return b / a;
        } 
        else if(token == "%")
        {
            return b % a;
        }
        else
        {
            Debug.Log($"not a valid operation token: {token}");
            return 0;
        }
    }
}