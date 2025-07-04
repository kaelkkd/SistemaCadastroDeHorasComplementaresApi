﻿using SistemaCadastroDeHorasApi.Models;
using SistemaCadastroDeHorasApi.Models.ENUMS;
using SistemaCadastroDeHorasApi.Repositories;

namespace SistemaCadastroDeHorasApi.Services.Factory;

public  class IntegralizacaoHorasFactory
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IAtividadesRepository _atividadesRepository;

    public IntegralizacaoHorasFactory(IUsuarioRepository usuarioRepository,
        IAtividadesRepository atividadesRepository)
    {
        _usuarioRepository = usuarioRepository; 
        _atividadesRepository = atividadesRepository;
        
    }

    public void alocarHoras(Guid atividadeId)
    {
        var atividade = _atividadesRepository.GetByIdAsync(atividadeId).Result;

        // Adicione esta verificação
        if (atividade == null)
        {
            throw new KeyNotFoundException($"Atividade com ID {atividadeId} não encontrada.");
        }

        var usuario = _usuarioRepository.GetByIdAsync(atividade.UsuarioId).Result;

        // Adicione esta verificação também, por segurança
        if (usuario == null)
        {
            throw new KeyNotFoundException($"Usuário associado à atividade {atividadeId} não encontrado.");
        }
        switch (atividade.tipoAtividadeComplementarHoras)
        {
            case TipoAtividadeComplementarHorasEnum.ParticipacaoOrganizacaoEventos:
                if (usuario.horasRestantesTotais > 0)
                {
                    if (usuario.horasRestantesDeParticipacaoOuOrganizacaoDeEventos > 0)
                    {
                        if (usuario.horasRestantesDeParticipacaoOuOrganizacaoDeEventos >= atividade.qtdHorasUtilizadas)
                        {
                            usuario.horasRestantesDeParticipacaoOuOrganizacaoDeEventos -= atividade.qtdHorasUtilizadas;
                            usuario.horasRestantesTotais = usuario.HorasTotais - atividade.qtdHorasUtilizadas;
                            _usuarioRepository.UpdateAsync(usuario);
                        }
                        else
                        {
                            throw new Exception("nao e possivel adiconar horas alem do total.");
                        }
                    }
                    else
                    {
                        throw new Exception("Horas de participação ou organização de eventos ja batidas.");
                    }
                }
                else
                {
                    throw new Exception("Horas restantes totais ja batidas.");
                }

                break;
            case TipoAtividadeComplementarHorasEnum.IniciacaoDocenciaPesquisaExtensao:
                if (usuario.horasRestantesTotais > 0)
                {
                    if (usuario.HorasTotaisDeIniciacaoADocenciaOuVivenciaOuExtensão > 0)
                    {
                        if (usuario.HorasTotaisDeIniciacaoADocenciaOuVivenciaOuExtensão >= atividade.qtdHorasUtilizadas)
                        {
                            usuario.HorasTotaisDeIniciacaoADocenciaOuVivenciaOuExtensão -= atividade.qtdHorasUtilizadas;
                            usuario.horasRestantesTotais = usuario.HorasTotais - atividade.qtdHorasUtilizadas;
                            _usuarioRepository.UpdateAsync(usuario);
                        }
                        else
                        {
                            throw new Exception("nao e possivel adiconar horas alem do total.");
                        }
                    }
                    else
                    {
                        throw new Exception("Horas de iniciação, docência, pesquisa ou extensão ja batidas.");
                    }
                }
                else
                {
                    throw new Exception("Horas totais ja batidas.");
                }

                break;
            case TipoAtividadeComplementarHorasEnum.AtividadesArtisticoCulturaisEsportivas:
                if (usuario.horasRestantesTotais > 0)
                {
                    if (usuario.horasRestantesDeAtividadesArtisticoCulturaisEEsportivas > 0)
                    {
                        if (usuario.horasRestantesDeAtividadesArtisticoCulturaisEEsportivas >=
                            atividade.qtdHorasUtilizadas)
                        {
                            usuario.horasRestantesDeAtividadesArtisticoCulturaisEEsportivas -=
                                atividade.qtdHorasUtilizadas;
                            usuario.horasRestantesTotais = usuario.HorasTotais - atividade.qtdHorasUtilizadas;
                            _usuarioRepository.UpdateAsync(usuario);
                        }
                        else
                        {
                            throw new Exception("nao e possivel adiconar horas alem do total.");
                        }
                    }
                    else
                    {
                        throw new Exception("Horas de atividades artistico culturais ou esportivas ja batidas.");
                    }
                }
                else
                {
                    throw new Exception("Horas totais ja batidas.");
                }

                break;
            case TipoAtividadeComplementarHorasEnum.ExperienciasProfissionais:
                if (usuario.horasRestantesTotais > 0)
                {
                    if (usuario.horasRestantesDeExperienciasLigadasAFormacaoProfissional > 0)
                    {
                        if (usuario.horasRestantesDeExperienciasLigadasAFormacaoProfissional >=
                            atividade.qtdHorasUtilizadas)
                        {
                            usuario.horasRestantesDeExperienciasLigadasAFormacaoProfissional -=
                                atividade.qtdHorasUtilizadas;
                            usuario.horasRestantesTotais = usuario.HorasTotais - atividade.qtdHorasUtilizadas;
                            _usuarioRepository.UpdateAsync(usuario);
                        }
                        else
                        {
                            throw new Exception("nao e possivel adiconar horas alem do total.");
                        }
                    }
                    else
                    {
                        throw new Exception("Horas de experiências profissionais ja batidas.");
                    }
                }
                else
                {
                    throw new Exception("Horas totais ja batidas.");
                }

                break;
            case TipoAtividadeComplementarHorasEnum.OutrasAtividades:
                if (usuario.horasRestantesTotais > 0)
                {
                    if (usuario.horasRestantesDeOutrasAtividades > 0)
                    {
                        if (usuario.horasRestantesDeOutrasAtividades >= atividade.qtdHorasUtilizadas)
                        {
                            usuario.horasRestantesDeOutrasAtividades -= atividade.qtdHorasUtilizadas;
                            usuario.horasRestantesTotais = usuario.HorasTotais - atividade.qtdHorasUtilizadas;
                            _usuarioRepository.UpdateAsync(usuario);
                        }
                        else
                        {
                            throw new Exception("nao e possivel adiconar horas alem do total.");
                        }
                    }
                    else
                    {
                        throw new Exception("Horas de outras atividades ja batidas.");
                    }
                }
                else
                {
                    throw new Exception("Horas totais ja batidas.");
                }

                break;
            case TipoAtividadeComplementarHorasEnum.VivenciasDeGestao:
                if (usuario.horasRestantesTotais > 0)
                {
                    if (usuario.horasRestantesDeVivenciasDeGestao > 0)
                    {
                        if (usuario.horasRestantesDeVivenciasDeGestao >= atividade.qtdHorasUtilizadas)
                        {
                            usuario.horasRestantesDeVivenciasDeGestao -= atividade.qtdHorasUtilizadas;
                            usuario.horasRestantesTotais = usuario.HorasTotais - atividade.qtdHorasUtilizadas;
                            _usuarioRepository.UpdateAsync(usuario);
                        }
                        else
                        {
                            throw new Exception("nao e possivel adiconar horas alem do total.");
                        }
                    }
                    else
                    {
                        throw new Exception("Horas de vivências de gestão ja batidas.");
                    }
                }
                else
                {
                    throw new Exception("Horas totais ja batidas.");
                }

                break;
            default:
                throw new Exception("Tipo de atividade complementar não reconhecido.");
        }
    }
}