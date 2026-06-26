import { prisma } from "../prisma/client";

type FilmeInput = {
  titulo: string;
  nota: number;
  imagem: string;
};

function validarFilme(dados: Partial<FilmeInput>): string | null {
  if (!dados.titulo || typeof dados.titulo !== "string" || dados.titulo.trim() === "") {
    return "O campo 'titulo' é obrigatório e deve ser uma string não vazia.";
  }
  if (!dados.imagem || typeof dados.imagem !== "string" || dados.imagem.trim() === "") {
    return "O campo 'imagem' é obrigatório e deve ser uma URL válida.";
  }
  if (dados.nota === undefined || dados.nota === null) {
    return "O campo 'nota' é obrigatório.";
  }
  if (typeof dados.nota !== "number" || !Number.isInteger(dados.nota)) {
    return "O campo 'nota' deve ser um número inteiro.";
  }
  if (dados.nota < 1 || dados.nota > 5) {
    return "O campo 'nota' deve ser um valor de 1 a 5 estrelas.";
  }
  return null;
}

export class FilmeService {

  async create(dados: FilmeInput) {
    const erro = validarFilme(dados);
    if (erro) throw new Error(erro);

    return await prisma.filme.create({
      data: dados,
    });
  }

  async delete(id: number) {
    const existe = await prisma.filme.findUnique({ where: { id } });
    if (!existe) throw new Error("Filme não encontrado.");

    return await prisma.filme.delete({
      where: { id },
    });
  }

  async findAll() {
    return await prisma.filme.findMany({
      orderBy: { createdAt: "desc" },
    });
  }

  async findById(id: number) {
    const filme = await prisma.filme.findUnique({ where: { id } });
    if (!filme) throw new Error("Filme não encontrado.");
    return filme;
  }

  async update(id: number, dados: FilmeInput) {
    const erro = validarFilme(dados);
    if (erro) throw new Error(erro);

    const existe = await prisma.filme.findUnique({ where: { id } });
    if (!existe) throw new Error("Filme não encontrado.");

    return await prisma.filme.update({
      where: { id },
      data: dados,
    });
  }
}
