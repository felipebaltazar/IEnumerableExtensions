# IEnumerableExtensions
 Extensões para IEnumerable


## Exemplo

```C#
    if (myList.IsNullOrEmpty())
    {
        DoSomething();
    }
```

```C#
    myList.Foreach(e => Console.WriteLine(e.ToString()));
```

```C#
    myList.ReverseForeach(e => Console.WriteLine(e.ToString()));
```

```C#
    var resultCollection = await myList.ForeachAsync(e => DoSomeAsyncTask(e));
```

```C#
    var taskCollection = myList.SelectAsync(e => DoSomeAsyncTask(e));
```

```C#
    TypeForCast result = myList.FirstCastOrDefault<TypeForCast>();
```