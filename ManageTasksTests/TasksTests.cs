using ManageTasks.Models;

namespace ManageTasksTests;

public class TasksTests
{
    [Fact(DisplayName = "Deveria retornar false se a data for menor que a data atual e true se for maior")]
    public void RetornarFalseSeADataDaTaskForMenorQueADataAtual()
    {
        var task = new Tasks();

        //Criando e verificando data 10 dias menores do que o dia atual
        task.Date = DateTime.Now.AddDays(-10);
        var result = task.DateTaskValid(task.Date);

        //Criando e verificando data 3 anos maiores do que o dia atual
        var date2 = DateTime.Now.AddYears(3);
        var result2 = task.DateTaskValid(date2);

        Assert.False(result);
        Assert.True(task.Date < DateTime.Now);
        Assert.False(result2);
        Assert.True(date2 > DateTime.Now.AddYears(2));
    }
}