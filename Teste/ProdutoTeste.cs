using ExpectedObjects;

namespace Teste
{
    public class ProdutoTeste
    {
        // propriedades
        private int    _codigo;
        private string _nome;
        private double _saldo;
        private double _custo;
        private string _medida;

        // m�todo construtor
        public ProdutoTeste() 
        {
            this._codigo = 1;
            this._nome = "Caneta";
            this._saldo = 0;
            this._custo = 0;
            this._medida = "PC";
        }





    /*
     * Requisito
     * 
    Eu, como almoxarife, preciso cadastrar produtos para que sejam
    consumidos pelos funcin�rios da empresa

    *
    * Crit�rios de aceite?
    *
    1 - Um novo produto deve ter obrigatoriamente um c�digo �nico, nome,
        saldo 0, custo 0 e unidade de medida
    2 - As unidades de medida s�o PC, KG, MT, GR, LT, CX
    3 - Um produto PODE  ter uma descri��o
    4 - Um produto PODE ter um c�digo de barras EAN
     */

        [Fact]
        public void CriarObjeto()
        {
            /* 
             * Primeira forma - usando variaveis
             * 
            int codigo = 1;
            string nome = "Caneca";
            double saldo = 0;
            double custo = 0;
            string medida = "PC";

            Produto produto = new Produto(codigo, nome, saldo, custo, medida);

            Assert.Equal(codigo, produto.Codigo);
            Assert.Equal(nome, produto.Nome);
            Assert.Equal(saldo, produto.Saldo);
            Assert.Equal(custo, produto.Custo);
            Assert.Equal(medida, produto.Medida);
            */



            /*
             * Segunda forma - usando um objeto an�nimo
             * 
            var obj = new {
                codigo = 1,
                nome = "Caneca",
                saldo = (double)0,
                custo = (double)0,
                medida = "PC"
            };

            Produto produto = new Produto(obj.codigo, obj.nome, obj.saldo, obj.custo, obj.medida);

            Assert.Equal(obj.codigo, produto.Codigo);
            Assert.Equal(obj.nome, produto.Nome);
            Assert.Equal(obj.saldo, produto.Saldo);
            Assert.Equal(obj.custo, produto.Custo);
            Assert.Equal(obj.medida, produto.Medida);
            */


            /*
             * Terceira forma - usando o setup do teste
             *
            Produto produto = new Produto(this._codigo, this._nome, this._saldo, 
                this._custo, this._medida);

            Assert.Equal(this._codigo, produto.Codigo);
            Assert.Equal(this._nome, produto.Nome);
            Assert.Equal(this._saldo, produto.Saldo);
            Assert.Equal(this._custo, produto.Custo);
            Assert.Equal(this._medida, produto.Medida);
            */


            /*
             * Quarta forma - usando objeto an�nimo e ExpectedObject
             */
            var obj = new
            {
                Codigo = this._codigo,
                Nome = this._nome,
                Saldo = this._saldo,
                Custo = this._custo,
                Medida = this._medida
            };

            Produto produto = new Produto(
                obj.Codigo, obj.Nome, obj.Saldo, obj.Custo, obj.Medida);

            obj.ToExpectedObject().ShouldMatch( produto );
        }



        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ProdutoNomeInvalido(string nomeInvalido)
        {
            /*Assert.Throws<ArgumentException>( ()=>
                new Produto(this._codigo,nomeInvalido,
                this._saldo, this._custo, this._medida));
            */
            var mensagem = 
            Assert.Throws<ArgumentException>( ()=>
                new Produto(this._codigo,nomeInvalido,
                this._saldo, this._custo, this._medida)).Message;
            Assert.Equal("Nome invalido", mensagem);
        }   

        [Theory]
        [InlineData(-1)]
        [InlineData(1)]
        public void SaldoInvalido(int SaldoInvalido)
        {
            // Assert.Throws<ArgumentException>(
            //     ()=>
            //     new Produto(this._codigo, this._nome, SaldoInvalido, this._custo, this._medida));
            
            var mensagem = 
            Assert.Throws<ArgumentException>( ()=>
                new Produto(this._codigo, this._nome, SaldoInvalido, 
                this._custo, this._medida)).Message;
                Assert.Equal("Saldo invalido", mensagem);
            

            // Assert.Throws<ArgumentException>(()=>
            //     new Produto(this._codigo, this._nome, SaldoInvalido, this._custo, this._medida)).ComMensagem("Saldo invalido");
        }
    }

    internal class Produto
    {
        private int codigo;
        private string nome;
        private double saldo;
        private double custo;
        private string medida;

        public Produto(int codigo, string nome, double saldo, double custo, string medida)
        {
            if (string.IsNullOrEmpty(nome))  //  (nome == "" || nome == null)
            {
                throw new ArgumentException("Nome invalido");
            }

            if (saldo != 0)
            {
                throw new ArgumentException("Saldo diferente de zero");
            }

            this.Codigo = codigo;
            this.Nome = nome;
            this.Saldo = saldo;
            this.Custo = custo;
            this.Medida = medida;
        }

        public int Codigo { get => codigo; private set => codigo = value; }
        public string Nome { get => nome; private set => nome = value; }
        public double Saldo { get => saldo; private set => saldo = value; }
        public double Custo { get => custo; private set => custo = value; }
        public string Medida { get => medida; private set => medida = value; }
    }
}