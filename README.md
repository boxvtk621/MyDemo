# MyDemo
Сборник моих практик и примеров. Этот проект - моя записная книжка, в которую я складываю интересные решения с которыми сталкиваюсь в практике. Второе назначение это набор шаблонов и паттернов.

## HttpLoggerMiddleware
Предназначен для отладочных работ. Если возникла необходимость логировать входящие запросы, значит, что-то в этой жизни у вас идет не так.

## Идентификаторы операций
Позволяет присваивать сквозной идентификатор операции

```cs
public void ConfigureServices(IServiceCollection services){
	services.AddCorrelationContext();
}

public void Configure(IApplicationBuilder app, IWebHostEnvironment env){
	app.UseOperationContext();
}
```